using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Providers.Mail.Dtos;
using AMAK.Application.Services.Notification;
using AMAK.Application.Services.Notification.Dtos;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Payment.Commands.Momo {
    public class HandlerMomoRequestCommandHandler : IRequestHandler<HandlerMomoRequestCommand, Response<string>> {
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IRepository<Option> _optionRepository;
        private readonly INotificationService _notificationService;
        private readonly IMailService _mailService;


        public HandlerMomoRequestCommandHandler(IRepository<Domain.Models.Order> orderRepository, IMailService mailService, IRepository<OrderDetail> orderDetailRepository, IRepository<Option> optionRepository, INotificationService notificationService) {
            _orderRepository = orderRepository;
            _mailService = mailService;
            _orderDetailRepository = orderDetailRepository;
            _optionRepository = optionRepository;
            _notificationService = notificationService;
        }

        public async Task<Response<string>> Handle(HandlerMomoRequestCommand request, CancellationToken cancellationToken) {

            var existingOrder = await _orderRepository.GetById(request.Data.OrderId)
            ?? throw new NotFoundException("Order not found!");

            var orderDetails = await _orderDetailRepository.GetAll()
                .Where(x => x.OrderId == existingOrder.Id)
                .ToListAsync(cancellationToken: cancellationToken);

            switch (request.Data.ResultCode) {
                case EMomoResultCode.Error:
                    try {
                        await _orderRepository.BeginTransactionAsync();

                        foreach (var detail in orderDetails) {
                            var existingOption = await _optionRepository.GetById(detail.OptionId)
                                ?? throw new NotFoundException("Option not found!");

                            existingOption.Quantity += detail.Quantity;

                            await _optionRepository.SaveChangesAsync();
                        }

                        _orderRepository.Remove(existingOrder);

                        await _orderRepository.SaveChangesAsync();
                        await _orderRepository.CommitTransactionAsync();
                    } catch (Exception) {
                        await _orderRepository.RollbackTransactionAsync();
                        throw new BadRequestException("Query wrong!");
                    }
                    break;

                case EMomoResultCode.Success:
                    var confirmNotification = new CreateNotificationForAccountRequest() {
                        Content = $"<p>Đơn hàng <b style=\"color: #16a34a;\">{existingOrder.Id}</b> đã được đặt thành công!</p>",
                        Url = $"/orders/{existingOrder.Id}",
                        IsGlobal = false,
                        UserId = existingOrder.UserId!
                    };

                    await _notificationService.CreateNotification(confirmNotification);
                    await _mailService.SendOrderMail(CreateMailTemplate(existingOrder.Email, "Xác nhận đơn hàng", existingOrder, orderDetails));
                    break;

                default:
                    throw new BadRequestException("Invalid ResultCode !");
            }

            return new Response<string>(HttpStatusCode.OK, "Handler Order Successfully!");

        }

        private static OrderMailEvent CreateMailTemplate(string email, string subject, Domain.Models.Order order, List<OrderDetail> orderDetails) {
            return new OrderMailEvent() {
                To = email,
                Subject = subject,
                Order = order,
                OrderResponses = orderDetails
            };
        }
    }
}