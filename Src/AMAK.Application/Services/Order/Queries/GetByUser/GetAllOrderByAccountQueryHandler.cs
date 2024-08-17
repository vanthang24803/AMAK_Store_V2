
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

            var orderQuery = _orderRepository
                    .GetAll()
                    .Where(x => !x.IsDeleted && x.UserId == existingAccount.Id)
                    .OrderByDescending(x => x.CreateAt)
                    .AsQueryable();

            var query = request.Query;

            if (!string.IsNullOrEmpty(query.OrderBy)) {
                orderQuery = query.OrderBy switch {
                    "All" => orderQuery,
                    "Pending" => orderQuery.Where(x => x.Status == EOrderStatus.PENDING),
                    "Create" => orderQuery.Where(x => x.Status == EOrderStatus.CREATE),
                    "Shipping" => orderQuery.Where(x => x.Status == EOrderStatus.SHIPPING),
                    "Success" => orderQuery.Where(x => x.Status == EOrderStatus.SUCCESS),
                    "Cancel" => orderQuery.Where(x => x.Status == EOrderStatus.CANCEL),
                    "Return" => orderQuery.Where(x => x.Status == EOrderStatus.RETURN),
                    _ => orderQuery
                };
            }

            var totalOrders = await orderQuery.CountAsync(cancellationToken: cancellationToken);

            var totalPages = (int)Math.Ceiling(totalOrders / (double)query.Limit);

            var orders = await orderQuery
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync(cancellationToken: cancellationToken);


            var response = new List<OrderResponse>();

            foreach (var order in orders) {

                var details = await _orderDetailRepository.GetAll()
                        .Where(x => x.OrderId == order.Id)
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
                    Id = order.Id,
                    Customer = order.Customer,
                    Email = order.Email,
                    Status = order.Status,
                    Address = order.Address,
                    NumberPhone = order.NumberPhone!,
                    IsReviewed = order.IsReviewed,
                    Payment = order.Payment,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    OrderDetails = details
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