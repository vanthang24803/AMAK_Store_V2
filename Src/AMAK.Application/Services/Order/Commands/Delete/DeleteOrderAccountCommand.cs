using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using MediatR;

namespace AMAK.Application.Services.Order.Commands.Delete {
    public class DeleteOrderAccountCommand(ClaimsPrincipal user, Guid id) : IRequest<Response<string>> {
        public ClaimsPrincipal User { get; set; } = user;
        public Guid Id { get; set; } = id;
    }
}