using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Payment.Commands.Momo {
    public class PaymentMomoCommand(ClaimsPrincipal user, CreateOrderRequest data) : IRequest<Response<string>> {

        public ClaimsPrincipal User { get; set; } = user;
        public CreateOrderRequest Data { get; set; } = data;
    }
}