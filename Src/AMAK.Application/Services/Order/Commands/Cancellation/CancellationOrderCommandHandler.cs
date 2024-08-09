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

        public CancellationOrderCommandHandler(UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository, IRepository<OrderDetail> orderDetailRepository, IRepository<Option> optionRepository) {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _optionRepository = optionRepository;
        }

        public async Task<Response<string>> Handle(CancellationOrderCommand request, CancellationToken cancellationToken) {

            var existingAccount = await _userManager.GetUserAsync(request.User) ?? throw new UnauthorizedException();

            var existingOrder = await _orderRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == existingAccount.Id, cancellationToken: cancellationToken) ?? throw new NotFoundException("Order not found!");

            var orderDetails = await _orderDetailRepository.GetAll().Where(x => x.OrderId == existingOrder.Id).ToListAsync(cancellationToken: cancellationToken);

            await _orderRepository.BeginTransactionAsync();
            try {
                if (existingOrder.Status != Domain.Enums.EOrderStatus.PENDING) {
                    throw new BadRequestException("Can't update to CREATE status from the current status!");
                }

                foreach (var detail in orderDetails) {
                    var existingOption = await _optionRepository.GetById(detail.OptionId) ?? throw new NotFoundException("Option not found");

                    existingOption.Quantity += detail.Quantity;

                    await _optionRepository.SaveChangesAsync();
                }

                existingOrder.Status = Domain.Enums.EOrderStatus.CANCEL;

                await _orderRepository.SaveChangesAsync();
                await _orderRepository.CommitTransactionAsync();

            } catch (Exception) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query wrong!");
            }

            return new Response<string>(HttpStatusCode.OK, "Cancellation order successfully!");
        }
    }
}