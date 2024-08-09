using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Order.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Queries.GetOrderById {
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderResponse>> {

        private readonly IRepository<Domain.Models.Order> _orderRepository;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;

        public GetOrderByIdQueryHandler(IRepository<Domain.Models.OrderDetail> orderDetailRepository, IRepository<Domain.Models.Order> orderRepository) {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Response<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken) {
            var existingOrder = await _orderRepository.GetAll()
              .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted) ?? throw new NotFoundException("Order not found!");

            var details = await _orderDetailRepository.GetAll()
                       .Where(x => x.OrderId == existingOrder.Id)
                       .Select(x => new OrderDetailResponse {
                           ProductId = x.ProductId,
                           Name = x.ProductName,
                           OptionName = x.OptionName,
                           Price = x.Price,
                           Quantity = x.Quantity,
                           Sale = x.Sale,
                           Thumbnail = x.Thumbnail
                       })
                       .ToListAsync(cancellationToken: cancellationToken);

            var orderResponse = new OrderResponse() {
                Id = existingOrder.Id,
                Customer = existingOrder.Customer,
                Address = existingOrder.Address,
                Email = existingOrder.Email,
                Status = existingOrder.Status,
                NumberPhone = existingOrder.NumberPhone!,
                Payment = existingOrder.Payment,
                Quantity = existingOrder.Quantity,
                TotalPrice = existingOrder.TotalPrice,
                OrderDetails = details,
                CreateAt = existingOrder.CreateAt,
                UpdateAt = existingOrder.UpdateAt
            };


            return new Response<OrderResponse>(HttpStatusCode.OK, orderResponse);
        }
    }
}