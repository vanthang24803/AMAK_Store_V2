using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Payment.Commands.Momo {
    public class HandlerMomoRequestCommandHandler : IRequestHandler<HandlerMomoRequestCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Order> _orderRepository;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;
        private readonly IRepository<Option> _optionRepository;

        private readonly IRepository<Domain.Models.Notification> _notificationRepository;

        private readonly IMailService _mailService;


        public HandlerMomoRequestCommandHandler(IRepository<Domain.Models.Order> orderRepository, IRepository<Domain.Models.Notification> notificationRepository, IMailService mailService, IRepository<Domain.Models.OrderDetail> orderDetailRepository, IRepository<Option> optionRepository) {
            _orderRepository = orderRepository;
            _notificationRepository = notificationRepository;
            _mailService = mailService;
            _orderDetailRepository = orderDetailRepository;
            _optionRepository = optionRepository;
        }

        public async Task<Response<string>> Handle(HandlerMomoRequestCommand request, CancellationToken cancellationToken) {

            var existingOrder = await _orderRepository.GetById(request.Data.OrderId) ?? throw new NotFoundException("Order not found!");

            var orderDetails = await _orderDetailRepository.GetAll().Where(x => x.OrderId == existingOrder.Id).ToListAsync(cancellationToken: cancellationToken);

            if (request.Data.ResultCode == EMomoResultCode.Error) {
                try {
                    await _orderRepository.BeginTransactionAsync();

                    foreach (var detail in orderDetails) {
                        var existingOption = await _optionRepository.GetById(detail.OptionId) ?? throw new NotFoundException("Option not found");

                        existingOption.Quantity += detail.Quantity;

                        await _optionRepository.SaveChangesAsync();
                    }

                    existingOrder.Status = EOrderStatus.CANCEL;

                    await _orderRepository.SaveChangesAsync();
                    await _orderRepository.CommitTransactionAsync();

                } catch (Exception) {
                    await _orderRepository.RollbackTransactionAsync();
                    throw new BadRequestException("Query wrong!");
                }
            }

            throw new NotImplementedException();
        }
    }
}