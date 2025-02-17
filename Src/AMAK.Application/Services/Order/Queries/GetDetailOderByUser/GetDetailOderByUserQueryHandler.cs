using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Order.Queries.GetDetailOderByUser {
    public class GetDetailOderByUserQueryHandler : IRequestHandler<GetDetailOderByUserQuery, Response<OrderResponse>> {
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;

        public GetDetailOderByUserQueryHandler(
            IRepository<Domain.Models.OrderDetail> orderDetailRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Domain.Models.Order> orderRepository) {
            _orderDetailRepository = orderDetailRepository;
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<Response<OrderResponse>> Handle(GetDetailOderByUserQuery request, CancellationToken cancellationToken) {
            var existingAccount = await _userManager.GetUserAsync(request.Claims) ?? throw new UnauthorizedException();

            var orderWithStatus = await _orderRepository
                .GetAll()
                .Include(s => s.Status)
                .Where(x => !x.IsDeleted && x.UserId == existingAccount.Id && x.Id == request.OrderId)
                .Select(o => new {
                    Order = o,
                    LatestStatus = o.Status
                        .OrderByDescending(st => st.TimeStamp)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new NotFoundException("Order not found!");

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

            return new Response<OrderResponse>(System.Net.HttpStatusCode.OK, orderResponse);
        }
    }
}