using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Queries.GetByUser {
    public class GetAllOrderByAccountQuery(OrderQuery query, ClaimsPrincipal claims) : IRequest<PaginationResponse<List<OrderResponse>>> {
        public OrderQuery Query { get; set; } = query;

        public ClaimsPrincipal Claims { get; set; } = claims;
    }
}