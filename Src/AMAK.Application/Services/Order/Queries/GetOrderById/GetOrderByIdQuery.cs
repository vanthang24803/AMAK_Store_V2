
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Order.Dtos;
using MediatR;

namespace AMAK.Application.Services.Order.Queries.GetOrderById {
    public class GetOrderByIdQuery(Guid id) : IRequest<Response<OrderResponse>> {
        public Guid Id { get; set; } = id;
    }
}