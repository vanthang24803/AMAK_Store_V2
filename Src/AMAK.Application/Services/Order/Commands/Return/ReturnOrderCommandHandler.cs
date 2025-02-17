using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Commands.Return {
    public class ReturnOrderCommandHandler : IRequestHandler<ReturnOrderCommand, Response<string>> {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly IRepository<OrderStatus> _orderStatusRepository;
        private readonly IRepository<OrderRefund> _orderRefundRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public ReturnOrderCommandHandler(IRepository<OrderStatus> orderStatusRepository, IRepository<Domain.Models.Order> orderRepository, UserManager<ApplicationUser> userManager, IRepository<OrderRefund> orderRefundRepository, IRepository<OrderDetail> orderDetailRepository) {
            _orderStatusRepository = orderStatusRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _orderRefundRepository = orderRefundRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Response<string>> Handle(ReturnOrderCommand request, CancellationToken cancellationToken) {
            var existingAccount = await _userManager.GetUserAsync(request.User) ?? throw new UnauthorizedException();

            var existingOrder = await _orderRepository.GetAll()
                    .Include(s => s.Status)
                    .FirstOrDefaultAsync(x => x.Id == request.Order.Id && x.UserId == existingAccount.Id, cancellationToken: cancellationToken)
                    ?? throw new NotFoundException("Order not found!");

            var latestStatus = existingOrder.Status
                    .OrderByDescending(s => s.TimeStamp)
                    .FirstOrDefault() ?? throw new NotFoundException("Order status not found!");

            await _orderRepository.BeginTransactionAsync();
            try {
                if (latestStatus.Status != Domain.Enums.EOrderStatus.SUCCESS) {
                    throw new BadRequestException("Cannot cancel the order as it is no longer in SUCCESS status.");
                }

                foreach (var item in request.Order.OrderDetails) {
                    var existingOrderDetail = await _orderDetailRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.OrderId == request.Order.Id
                    && x.ProductId == item.ProductId
                    && x.OptionId == item.OptionId,
                    cancellationToken: cancellationToken)
                     ?? throw new NotFoundException("Order detail not found!");

                    existingOrderDetail.IsRefund = true;
                }

                await _orderDetailRepository.SaveChangesAsync();

                var returnStatus = new OrderStatus() {
                    OrderId = existingOrder.Id,
                    Status = Domain.Enums.EOrderStatus.RETURN,
                    TimeStamp = DateTime.UtcNow
                };

                _orderStatusRepository.Add(returnStatus);

                await _orderStatusRepository.SaveChangesAsync();

                await _orderRepository.SaveChangesAsync();
                await _orderRepository.CommitTransactionAsync();

            } catch (Exception ex) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException($"An error occurred: {ex.Message}");
            }

            var newRefundOrder = new OrderRefund() {
                OrderId = existingOrder.Id,
                Message = "",
                IsAccepted = false,
            };

            _orderRefundRepository.Add(newRefundOrder);

            return new Response<string>(HttpStatusCode.OK, "Cancellation order successfully!");
        }
    }
}