using MediatR;
using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;

namespace AMAK.Application.Services.Order.Commands.Return {
    public class ReturnOrderCommand(ClaimsPrincipal user, OrderResponse order) : IRequest<Response<string>> {
        public ClaimsPrincipal User { get; set; } = user;
        public OrderResponse Order { get; set; } = order;
    }
}