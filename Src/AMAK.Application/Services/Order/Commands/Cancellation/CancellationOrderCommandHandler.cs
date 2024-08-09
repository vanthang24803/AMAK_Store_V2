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

        public CancellationOrderCommandHandler(UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository) {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<Response<string>> Handle(CancellationOrderCommand request, CancellationToken cancellationToken) {

            var existingAccount = await _userManager.GetUserAsync(request.User) ?? throw new UnauthorizedException();

            var existingOrder = await _orderRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == existingAccount.Id, cancellationToken: cancellationToken) ?? throw new NotFoundException("Order not found!");

            if (existingOrder.Status != Domain.Enums.EOrderStatus.PENDING) {
                throw new BadRequestException("Can't update to CREATE status from the current status!");
            }

            existingOrder.Status = Domain.Enums.EOrderStatus.CANCEL;

            await _orderRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Cancellation order successfully!");
        }
    }
}