using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Queries.GetAllOrder {
    public class GetAllOrderQuery(BaseQuery query) : IRequest<PaginationResponse<List<OrderResponse>>> {
        public BaseQuery Query { get; set; } = query;
    }
}