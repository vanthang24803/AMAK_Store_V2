using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Commands.Update {
    public class UpdateOrderByAccountCommand(ClaimsPrincipal user, Guid id, UpdateOrderByAccountRequest data) : IRequest<Response<string>> {

        public ClaimsPrincipal User { get; set; } = user;

        public Guid Id { get; set; } = id;

        public UpdateOrderByAccountRequest Data { get; set; } = data;
    }
}