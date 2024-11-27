using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Providers.Mail.Dtos;
using AMAK.Application.Providers.RabbitMq;
using AMAK.Application.Providers.RabbitMq.Common;
using AMAK.Application.Services.Notification;
using AMAK.Application.Services.Notification.Dtos;
using AMAK.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Order.Commands.Update {
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Response<string>> {
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly INotificationService _notificationService;
        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;
        private readonly IRepository<Domain.Models.OrderStatus> _orderStatusRepository;
        private readonly IRabbitProducer _rabbitProducer;


        public UpdateOrderStatusCommandHandler(IRepository<Domain.Models.Order> orderRepository, INotificationService notificationService, IRepository<Domain.Models.OrderDetail> orderDetailRepository, IRepository<Domain.Models.OrderStatus> orderStatusRepository, IRabbitProducer rabbitProducer) {
            _orderRepository = orderRepository;
            _notificationService = notificationService;
            _orderDetailRepository = orderDetailRepository;
            _orderStatusRepository = orderStatusRepository;
            _rabbitProducer = rabbitProducer;
        }

        public async Task<Response<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken) {

            var existingOrder = await _orderRepository.GetAll()
                    .Include(s => s.Status)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException("Order not found!");

            var orderDetails = await _orderDetailRepository.GetAll().Where(x => x.OrderId == existingOrder.Id).ToListAsync(cancellationToken: cancellationToken);

            await _orderRepository.BeginTransactionAsync();

            var latestStatus = existingOrder.Status
                .OrderByDescending(s => s.TimeStamp)
                .FirstOrDefault() ?? throw new NotFoundException("Order status not found!");

            try {
                switch (request.Data.Status) {
                    case EOrderStatus.CREATE:
                        ValidateCurrentStatus(latestStatus.Status, EOrderStatus.PENDING, "CREATE");

                        existingOrder = await HandlerStatus(existingOrder, request.Data.Status);

                        CreateAndSendNotification(existingOrder, "Đơn hàng đã được xác nhận!");
                        _rabbitProducer.SendMessage(RabbitQueue.OrderQueue, CreateMailTemplate(existingOrder.Email, "Đơn hàng đã được khởi tạo!", existingOrder, orderDetails));
                        break;

                    case EOrderStatus.CANCEL:
                        ValidateCurrentStatus(latestStatus.Status, EOrderStatus.PENDING, "CANCEL");
                        existingOrder = await HandlerStatus(existingOrder, request.Data.Status);
                        CreateAndSendNotification(existingOrder, "Đơn hàng đã hủy thành công!");
                        _rabbitProducer.SendMessage(RabbitQueue.OrderQueue, CreateMailTemplate(existingOrder.Email, "Bạn đã hủy đơn hàng thành công!", existingOrder, orderDetails));
                        break;

                    case EOrderStatus.SHIPPING:
                        ValidateCurrentStatus(latestStatus.Status, EOrderStatus.CREATE, "SHIPPING");
                        existingOrder = await HandlerStatus(existingOrder, request.Data.Status);
                        CreateAndSendNotification(existingOrder, "Đơn hàng đang được vận chuyển!");
                        _rabbitProducer.SendMessage(RabbitQueue.OrderQueue, CreateMailTemplate(existingOrder.Email, "Đơn hàng của bạn đang được vận chuyển!", existingOrder, orderDetails));
                        break;

                    case EOrderStatus.SUCCESS:
                        ValidateCurrentStatus(latestStatus.Status, EOrderStatus.SHIPPING, "SUCCESS");
                        existingOrder = await HandlerStatus(existingOrder, request.Data.Status);
                        CreateAndSendNotification(existingOrder, "Đơn hàng đã giao tới bạn thành công!");
                        _rabbitProducer.SendMessage(RabbitQueue.OrderQueue, CreateMailTemplate(existingOrder.Email, "Đơn hàng đã giao tới bạn thành công!", existingOrder, orderDetails));
                        break;

                    default:
                        throw new BadRequestException("Invalid order status!");
                }

                await _orderRepository.SaveChangesAsync();
                await _orderRepository.CommitTransactionAsync();

            } catch (Exception) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query wrong!");
            }

            return new Response<string>(HttpStatusCode.OK, "Update Success!");
        }

        private static void ValidateCurrentStatus(EOrderStatus currentStatus, EOrderStatus expectedStatus, string newStatus) {
            if (currentStatus != expectedStatus) {
                throw new BadRequestException($"Can't update to {newStatus} status from the current status!");
            }
        }

        private async Task<Domain.Models.Order> HandlerStatus(Domain.Models.Order existingOrder, EOrderStatus request) {
            var status = existingOrder.Status.FirstOrDefault(x => x.Status == request);

            if (status == null) {
                status = new Domain.Models.OrderStatus {
                    OrderId = existingOrder.Id,
                    Status = request,
                    TimeStamp = DateTime.UtcNow
                };

                _orderStatusRepository.Add(status);
                await _orderStatusRepository.SaveChangesAsync();

                existingOrder.Status.Add(status);
            } else {
                status.TimeStamp = DateTime.UtcNow;

                _orderStatusRepository.Update(status);
                await _orderStatusRepository.SaveChangesAsync();
            }

            return existingOrder;
        }

        private void CreateAndSendNotification(Domain.Models.Order order, string message) {
            var notification = new CreateNotificationForAccountRequest() {
                Content = $"<p>{message} <b style=\"color: #16a34a;\">{order.Id}</b></p>",
                Url = $"/orders/{order.Id}",
                IsGlobal = false,
                UserId = order.UserId!
            };

            _rabbitProducer.SendMessage(RabbitQueue.Notification, notification);
        }

        private static OrderMailEvent CreateMailTemplate(string email, string subject, Domain.Models.Order order, List<Domain.Models.OrderDetail> orderDetails) {

            return new OrderMailEvent() {
                To = email,
                Subject = subject,
                Order = order,
                OrderResponses = orderDetails
            };
        }
    }
}