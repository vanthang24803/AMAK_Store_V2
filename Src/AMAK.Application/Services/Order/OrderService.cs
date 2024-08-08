using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Models;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AMAK.Application.Common.Query;
using AMAK.Domain.Enums;
using AMAK.Application.Services.Notification;
using AMAK.Application.Services.Notification.Dtos;

namespace AMAK.Application.Services.Order {
    public class OrderService : IOrderService {
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        public readonly UserManager<ApplicationUser> _userManager;

        private readonly IMailService _mailService;

        private readonly IRepository<Domain.Models.Voucher> _voucherRepository;

        private readonly IRepository<Domain.Models.Option> _optionRepository;

        private readonly IRepository<Domain.Models.OrderDetail> _orderDetailRepository;

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly INotificationService _notificationService;

        public OrderService(IMailService mailService, UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository, IRepository<Voucher> voucherRepository, IRepository<Option> optionRepository, IRepository<Domain.Models.OrderDetail> orderDetailRepository, IRepository<Domain.Models.Product> productRepository, INotificationService notificationService) {
            _mailService = mailService;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
            _optionRepository = optionRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _notificationService = notificationService;
        }

        public async Task<Response<string>> CreateAsync(ClaimsPrincipal user, CreateOrderRequest request) {
            var existingAccount = await _userManager.GetUserAsync(user) ?? throw new UnauthorizedException();

            var newOrder = new Domain.Models.Order {
                Id = request.Id,
                Email = request.Email,
                Customer = request.Customer,
                Address = request.Address,
                Payment = request.Payment,
                Quantity = request.Quantity,
                Shipping = true,
                NumberPhone = request.NumberPhone,
                Status = EOrderStatus.PENDING,
                TotalPrice = request.TotalPrice,
                UserId = existingAccount.Id,
            };

            _orderRepository.Add(newOrder);

            await _orderRepository.SaveChangesAsync();

            var orderDetails = new List<Domain.Models.OrderDetail>();

            foreach (var item in request.Products) {

                var existingProduct = await _productRepository.GetById(item.ProductId) ?? throw new NotFoundException("Product not found!");

                var existingOption = await _optionRepository.GetAll().Include(x => x.Product).FirstOrDefaultAsync(
                    x => x.Id == item.OptionId && x.ProductId == existingProduct.Id
                ) ?? throw new NotFoundException("Option not found!");

                existingOption.Quantity -= item.Quantity;

                await _optionRepository.SaveChangesAsync();

                existingProduct.Sold += item.Quantity;

                await _productRepository.SaveChangesAsync();

                var newOrderDetail = new Domain.Models.OrderDetail() {
                    OrderId = newOrder.Id,
                    OptionId = item.OptionId,
                    ProductName = item.ProductName,
                    OptionName = item.OptionName,
                    ProductId = item.ProductId,
                    Thumbnail = item.Thumbnail,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Sale = item.Sale
                };

                orderDetails.Add(newOrderDetail);
            }

            _orderDetailRepository.AddRange(orderDetails);

            await _orderDetailRepository.SaveChangesAsync();

            if (!request.Voucher.IsNullOrEmpty()) {
                var existingVoucher = await _voucherRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Code.Equals(request.Voucher)) ?? throw new NotFoundException("Voucher not found!");

                existingVoucher.Quantity--;

                if (existingVoucher.Quantity == 0) {
                    existingVoucher.IsExpire = true;
                }

                await _voucherRepository.SaveChangesAsync();
            }

            var confirmNotification = new CreateNotificationForAccountRequest() {
                Content = $"<p>Đơn hàng <b style=\"color: #16a34a;\">{newOrder.Id}</b> đã được đặt thành công!</p>",
                Url = $"/orders/{newOrder.Id}",
                IsGlobal = false,
                UserId = existingAccount.Id
            };

            await _notificationService.CreateNotification(confirmNotification);

            await _mailService.SendOrderMail(request.Email, "Xác nhận đơn hàng", newOrder, orderDetails);

            return new Response<string>(HttpStatusCode.Created, "Product created Successfully!");
        }

        public async Task<PaginationResponse<List<OrderResponse>>> GetByUser(ClaimsPrincipal user, OrderQuery query) {
            var existingAccount = await _userManager.GetUserAsync(user) ?? throw new UnauthorizedException();

            var orderQuery = _orderRepository
                    .GetAll()
                    .Where(x => !x.IsDeleted && x.UserId == existingAccount.Id)
                    .OrderByDescending(x => x.CreateAt)
                    .AsQueryable();

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

            var totalOrders = await orderQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(totalOrders / (double)query.Limit);

            var orders = await orderQuery
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();


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
                        .ToListAsync();

                var orderResponse = new OrderResponse() {
                    Id = order.Id,
                    Customer = order.Customer,
                    Email = order.Email,
                    Status = order.Status,
                    NumberPhone = order.NumberPhone!,
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

        public async Task<Response<OrderResponse>> GetAsync(Guid id) {

            var existingOrder = await _orderRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted) ?? throw new NotFoundException("Order not found!");

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
                       .ToListAsync();

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


        public async Task<Response<string>> DeleteAsync(ClaimsPrincipal user, Guid id) {
            var existingAccount = await _userManager.GetUserAsync(user)
                ?? throw new UnauthorizedException();

            var existingOrder = await _orderRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted && x.UserId == existingAccount.Id)
                ?? throw new NotFoundException("Order not found!");

            if (!existingOrder.Status.Equals(EOrderStatus.PENDING) && !existingOrder.Status.Equals(EOrderStatus.SUCCESS)) {
                throw new BadRequestException("Can't delete order!");
            }

            existingOrder.IsDeleted = true;
            existingOrder.DeleteAt = DateTime.UtcNow;

            await _orderRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Order deleted successfully!");
        }


    }
}