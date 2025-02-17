using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Queries.GetDetailOderByUser {
    public class GetDetailOderByUserQuery(Guid orderId, ClaimsPrincipal claims) : IRequest<Response<OrderResponse>> {
        public Guid OrderId { get; set; } = orderId;
        public ClaimsPrincipal Claims { get; set; } = claims;
    }
}