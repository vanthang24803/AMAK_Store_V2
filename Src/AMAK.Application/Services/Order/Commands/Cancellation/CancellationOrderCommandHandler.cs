using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Order.Commands.Cancellation {
    public class CancellationOrderCommandHandler : IRequestHandler<CancellationOrderCommand, Response<string>> {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        private readonly IRepository<Option> _optionRepository;
        private readonly IRepository<CancelOrder> _cancelOrderRepository;
        private readonly IRepository<OrderStatus> _orderStatusRepository;

        public CancellationOrderCommandHandler(UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository, IRepository<OrderDetail> orderDetailRepository, IRepository<Option> optionRepository, IRepository<CancelOrder> cancelOrderRepository, IRepository<OrderStatus> orderStatusRepository) {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _optionRepository = optionRepository;
            _cancelOrderRepository = cancelOrderRepository;
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<Response<string>> Handle(CancellationOrderCommand request, CancellationToken cancellationToken) {

            var existingAccount = await _userManager.GetUserAsync(request.User) ?? throw new UnauthorizedException();

            var existingOrder = await _orderRepository.GetAll()
                    .Include(s => s.Status)
                    .FirstOrDefaultAsync(x => x.Id == request.OrderCancel.OrderId && x.UserId == existingAccount.Id, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException("Order not found!");

            var orderDetails = await _orderDetailRepository.GetAll()
                    .Where(x => x.OrderId == existingOrder.Id).ToListAsync(cancellationToken: cancellationToken);

            var latestStatus = existingOrder.Status
                     .OrderByDescending(s => s.TimeStamp)
                     .FirstOrDefault() ?? throw new NotFoundException("Order status not found!");

            await _orderRepository.BeginTransactionAsync();
            try {
                if (latestStatus.Status != Domain.Enums.EOrderStatus.PENDING) {
                    throw new BadRequestException("Cannot cancel the order as it is no longer in PENDING status.");
                }

                foreach (var detail in orderDetails) {
                    var existingOption = await _optionRepository.GetById(detail.OptionId) ?? throw new NotFoundException("Option not found");

                    existingOption.Quantity += detail.Quantity;
                }

                var cancelStatus = new OrderStatus() {
                    OrderId = existingOrder.Id,
                    Status = Domain.Enums.EOrderStatus.CANCEL,
                    TimeStamp = DateTime.UtcNow
                };

                _orderStatusRepository.Add(cancelStatus);

                await _optionRepository.SaveChangesAsync();
                await _orderStatusRepository.SaveChangesAsync();
                await _orderRepository.SaveChangesAsync();
                await _orderRepository.CommitTransactionAsync();

            } catch (Exception ex) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException($"An error occurred: {ex.Message}");
            }

            var newCancelOrder = new CancelOrder() {
                OrderId = existingOrder.Id,
                Message = request.OrderCancel.Message,
            };

            _cancelOrderRepository.Add(newCancelOrder);
            await _cancelOrderRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Cancellation order successfully!");
        }

    }
}