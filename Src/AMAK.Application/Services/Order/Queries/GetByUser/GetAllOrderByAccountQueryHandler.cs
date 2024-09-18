
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Queries.GetByUser {
    public class GetAllOrderByAccountQueryHandler : IRequestHandler<GetAllOrderByAccountQuery, PaginationResponse<List<OrderResponse>>> {
        private readonly IRepository<Domain.Models.Order> _orderRepository;

        public readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;


        public GetAllOrderByAccountQueryHandler(IRepository<Domain.Models.OrderDetail> orderDetailRepository, UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository) {
            _orderDetailRepository = orderDetailRepository;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<PaginationResponse<List<OrderResponse>>> Handle(GetAllOrderByAccountQuery request, CancellationToken cancellationToken) {
            var existingAccount = await _userManager.GetUserAsync(request.Claims) ?? throw new UnauthorizedException();

            var query = request.Query;

            var orderQuery = _orderRepository
                .GetAll()
                .Include(s => s.Status)
                .Where(x => !x.IsDeleted && x.UserId == existingAccount.Id)
                .OrderByDescending(x => x.CreateAt)
                .AsSplitQuery();

            var ordersWithLatestStatus = orderQuery
                .Select(o => new {
                    Order = o,
                    LatestStatus = o.Status
                        .OrderByDescending(st => st.TimeStamp)
                        .FirstOrDefault()
                });

            if (!string.IsNullOrEmpty(query.OrderBy)) {
                ordersWithLatestStatus = query.OrderBy switch {
                    "All" => ordersWithLatestStatus,
                    "Pending" => ordersWithLatestStatus.Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.PENDING),
                    "Create" => ordersWithLatestStatus.Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.CREATE),
                    "Shipping" => ordersWithLatestStatus.Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SHIPPING),
                    "Success" => ordersWithLatestStatus.Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SUCCESS),
                    "Cancel" => ordersWithLatestStatus.Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.CANCEL),
                    "Return" => ordersWithLatestStatus.Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.RETURN),
                    _ => ordersWithLatestStatus
                };
            }


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