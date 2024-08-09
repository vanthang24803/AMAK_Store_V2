using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Services.Notification;
using AMAK.Application.Services.Notification.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Order.Commands.Update {
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Order> _orderRepository;

        private readonly IMailService _mailService;

        private readonly INotificationService _notificationService;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;

        public UpdateOrderStatusCommandHandler(IRepository<Domain.Models.Order> orderStatusRepository, INotificationService notificationService, IMailService mailService, IRepository<Domain.Models.OrderDetail> orderDetailRepository) {
            _orderRepository = orderStatusRepository;
            _notificationService = notificationService;
            _mailService = mailService;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Response<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken) {

            var existingOrder = await _orderRepository.GetById(request.Id) ?? throw new NotFoundException("Order not found!");

            var orderDetails = await _orderDetailRepository.GetAll().Where(x => x.OrderId == existingOrder.Id).ToListAsync(cancellationToken: cancellationToken);

            await _orderRepository.BeginTransactionAsync();
            try {
                switch (request.Data.Status) {
                    case Domain.Enums.EOrderStatus.CREATE:
                        ValidateCurrentStatus(existingOrder.Status, Domain.Enums.EOrderStatus.PENDING, "CREATE");
                        existingOrder.Status = request.Data.Status;
                        await CreateAndSendNotification(existingOrder, "Đơn hàng đã được xác nhận!");
                        await _mailService.SendOrderMail(existingOrder.Email, "Đơn hàng đã được khởi tạo!", existingOrder, orderDetails);
                        break;

                    case Domain.Enums.EOrderStatus.CANCEL:
                        ValidateCurrentStatus(existingOrder.Status, Domain.Enums.EOrderStatus.PENDING, "CANCEL");
                        existingOrder.Status = request.Data.Status;
                        await CreateAndSendNotification(existingOrder, "Đơn hàng đã hủy thành công!");
                        await _mailService.SendOrderMail(existingOrder.Email, "Bạn đã hủy đơn hàng thành công!", existingOrder, orderDetails);
                        break;

                    case Domain.Enums.EOrderStatus.SHIPPING:
                        ValidateCurrentStatus(existingOrder.Status, Domain.Enums.EOrderStatus.CREATE, "SHIPPING");
                        existingOrder.Status = request.Data.Status;
                        await CreateAndSendNotification(existingOrder, "Đơn hàng đang được vận chuyển!");
                        await _mailService.SendOrderMail(existingOrder.Email, "Đơn hàng của bạn đang được vận chuyển!", existingOrder, orderDetails);
                        break;

                    case Domain.Enums.EOrderStatus.SUCCESS:
                        ValidateCurrentStatus(existingOrder.Status, Domain.Enums.EOrderStatus.SHIPPING, "SUCCESS");
                        existingOrder.Status = request.Data.Status;
                        await CreateAndSendNotification(existingOrder, "Đơn hàng đã giao tới bạn thành công!");
                        await _mailService.SendOrderMail(existingOrder.Email, "Đơn hàng đã giao tới bạn thành công!", existingOrder, orderDetails);
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

        private static void ValidateCurrentStatus(Domain.Enums.EOrderStatus currentStatus, Domain.Enums.EOrderStatus expectedStatus, string newStatus) {
            if (currentStatus != expectedStatus) {
                throw new BadRequestException($"Can't update to {newStatus} status from the current status!");
            }
        }

        private async Task CreateAndSendNotification(Domain.Models.Order order, string message) {
            var notification = new CreateNotificationForAccountRequest() {
                Content = $"<p>{message} <b style=\"color: #16a34a;\">{order.Id}</b></p>",
                Url = $"/orders/{order.Id}",
                IsGlobal = false,
                UserId = order.UserId!
            };

            await _notificationService.CreateNotification(notification);
        }
    }
}