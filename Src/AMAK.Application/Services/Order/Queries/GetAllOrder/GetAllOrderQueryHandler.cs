
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Queries.GetAllOrder {
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, PaginationResponse<List<OrderResponse>>> {
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;
        public GetAllOrderQueryHandler(IRepository<Domain.Models.Order> orderRepository, IRepository<Domain.Models.OrderDetail> orderDetailRepository) {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<PaginationResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken) {
            var query = request.Query;

            var orderQuery = _orderRepository
                   .GetAll()
                   .Include(s => s.Status)
                   .OrderByDescending(x => x.CreateAt)
                   .AsSplitQuery();

            var ordersWithLatestStatus = orderQuery
             .Select(o => new {
                 Order = o,
                 LatestStatus = o.Status
                     .OrderByDescending(st => st.TimeStamp)
                     .FirstOrDefault()
             });

            var totalOrders = await ordersWithLatestStatus.CountAsync(cancellationToken: cancellationToken);
            var totalPages = (int)Math.Ceiling(totalOrders / (double)query.Limit);

            var paginatedOrders = await ordersWithLatestStatus
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync(cancellationToken: cancellationToken);

            var response = new List<OrderResponse>();

            foreach (var orderWithStatus in paginatedOrders) {
                var order = orderWithStatus.Order;

                var details = await _orderDetailRepository.GetAll()
                    .Where(x => x.OrderId == order.Id)
                    .Select(x => new OrderDetailResponse {
                        ProductId = x.ProductId,
                        Name = x.ProductName,
                        OptionName = x.OptionName,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Sale = x.Sale,
                        Thumbnail = x.Thumbnail,
                        OptionId = x.OptionId,
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                var orderResponse = new OrderResponse {
                    Id = order.Id,
                    Customer = order.Customer,
                    Email = order.Email,
                    Status = orderWithStatus.LatestStatus != null ? orderWithStatus.LatestStatus.Status : EOrderStatus.PENDING,
                    Address = order.Address,
                    NumberPhone = order.NumberPhone!,
                    IsReviewed = order.IsReviewed,
                    Payment = order.Payment,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    OrderDetails = details,
                    CreateAt = order.CreateAt,
                    UpdateAt = order.UpdateAt
                };

                response.Add(orderResponse);
            }

            var paginatedResponse = new PaginationResponse<List<OrderResponse>> {
                CurrentPage = query.Page,
                TotalPage = totalPages,
                Items = query.Limit,
                TotalItems = totalOrders,
                Result = response
            };

            return paginatedResponse;
        }
    }
}