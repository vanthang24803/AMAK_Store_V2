
using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Commands.Update {
    public class UpdateOrderByAccountCommandHandler : IRequestHandler<UpdateOrderByAccountCommand, Response<string>> {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Domain.Models.Order> _orderRepository;

        private readonly IMapper _mapper;

        public UpdateOrderByAccountCommandHandler(UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository, IMapper mapper) {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateOrderByAccountCommand request, CancellationToken cancellationToken) {
            try {
                await _orderRepository.BeginTransactionAsync();

                var existingAccount = await _userManager.GetUserAsync(request.User) ?? throw new UnauthorizedException();
                var existingOrder = await _orderRepository.GetAll().
                        FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == existingAccount.Id,
                        cancellationToken: cancellationToken)
                        ?? throw new NotFoundException("Order not found!");

                var latestStatus = existingOrder.Status
                     .OrderByDescending(s => s.TimeStamp)
                     .FirstOrDefault() ?? throw new NotFoundException("Order status not found!");

                if (existingOrder.IsDeleted && latestStatus.Status != Domain.Enums.EOrderStatus.PENDING) {
                    throw new BadRequestException("Only orders in initialization state can be edited!");
                }

                _mapper.Map(request.Data, existingOrder);
                await _orderRepository.SaveChangesAsync();

                await _orderRepository.CommitTransactionAsync();

                return new Response<string>(HttpStatusCode.OK, "Update order successfully!");
            } catch (Exception) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query wrong!");
            }
        }

    }
}