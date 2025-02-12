using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Commands.Cancellation.Dto;
using MediatR;

namespace AMAK.Application.Services.Order.Commands.Cancellation {
    public class CancellationOrderCommand(ClaimsPrincipal user, OrderCancelRequest request) : IRequest<Response<string>> {
        public ClaimsPrincipal User { get; set; } = user;

        public OrderCancelRequest OrderCancel { get; set; } = request;
    }
}