using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Commands.Delete {
    public class DeleteOrderAccountCommandHandler : IRequestHandler<DeleteOrderAccountCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Order> _orderRepository;
        public readonly UserManager<ApplicationUser> _userManager;

        public DeleteOrderAccountCommandHandler(UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository) {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<Response<string>> Handle(DeleteOrderAccountCommand request, CancellationToken cancellationToken) {
            var existingAccount = await _userManager.GetUserAsync(request.User)
               ?? throw new UnauthorizedException();

            var existingOrder = await _orderRepository.GetAll()
                .Include(s => s.Status)
                .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted && x.UserId == existingAccount.Id, cancellationToken: cancellationToken)
                ?? throw new NotFoundException("Order not found!");

            var latestStatus = existingOrder.Status
                    .OrderByDescending(s => s.TimeStamp)
                    .FirstOrDefault() ?? throw new NotFoundException("Order status not found!");

            if (!latestStatus.Status.Equals(EOrderStatus.PENDING) && !latestStatus.Status.Equals(EOrderStatus.SUCCESS)) {
                throw new BadRequestException("Can't delete order!");
            }

            existingOrder.IsDeleted = true;
            existingOrder.DeleteAt = DateTime.UtcNow;

            await _orderRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Order deleted successfully!");
        }
    }
}