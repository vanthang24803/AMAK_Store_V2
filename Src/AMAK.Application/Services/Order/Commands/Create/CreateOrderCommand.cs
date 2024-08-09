using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Commands.Create {
    public class CreateOrderCommand : IRequest<Response<string>> {
        public ClaimsPrincipal User { get; set; } = null!;

        public CreateOrderRequest Data { get; set; } = null!;

        public CreateOrderCommand(ClaimsPrincipal user, CreateOrderRequest data) {
            User = user;
            Data = data;
        }
    }
}