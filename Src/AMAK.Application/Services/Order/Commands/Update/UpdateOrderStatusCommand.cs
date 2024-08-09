using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Commands.Update {
    public class UpdateOrderStatusCommand(Guid id, UpdateStatusOrderRequest data) : IRequest<Response<string>> {
        public Guid Id { get; set; } = id;
        public UpdateStatusOrderRequest Data { get; set; } = data;
    }
}