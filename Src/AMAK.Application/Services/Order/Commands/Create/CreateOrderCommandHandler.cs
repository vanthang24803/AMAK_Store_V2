using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Mail;
using AMAK.Application.Services.Notification;
using AMAK.Application.Services.Notification.Dtos;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AMAK.Application.Services.Order.Commands.Create {
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<string>> {
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;

        private readonly IRepository<Voucher> _voucherRepository;

        private readonly IRepository<Option> _optionRepository;

        private readonly IRepository<OrderDetail> _orderDetailRepository;

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly INotificationService _notificationService;

        public CreateOrderCommandHandler(IMailService mailService, UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository, IRepository<Voucher> voucherRepository, IRepository<Option> optionRepository, IRepository<OrderDetail> orderDetailRepository, IRepository<Domain.Models.Product> productRepository, INotificationService notificationService) {
            _mailService = mailService;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
            _optionRepository = optionRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _notificationService = notificationService;
        }

        public async Task<Response<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken) {

            var user = request.User;
            var data = request.Data;

            var existingAccount = await _userManager.GetUserAsync(user) ?? throw new UnauthorizedException();

            await _orderRepository.BeginTransactionAsync();
            try {
                var newOrder = new Domain.Models.Order {
                    Id = data.Id,
                    Email = data.Email,
                    Customer = data.Customer,
                    Address = data.Address,
                    Payment = data.Payment,
                    Quantity = data.Quantity,
                    Shipping = true,
                    NumberPhone = data.NumberPhone,
                    Status = EOrderStatus.PENDING,
                    TotalPrice = data.TotalPrice,
                    UserId = existingAccount.Id,
                };

                _orderRepository.Add(newOrder);

                await _orderRepository.SaveChangesAsync();

                var orderDetails = new List<OrderDetail>();

                foreach (var item in data.Products) {

                    var existingProduct = await _productRepository.GetById(item.ProductId) ?? throw new NotFoundException("Product not found!");

                    var existingOption = await _optionRepository.GetAll().Include(x => x.Product).FirstOrDefaultAsync(
                        x => x.Id == item.OptionId && x.ProductId == existingProduct.Id
                    ) ?? throw new NotFoundException("Option not found!");

                    existingOption.Quantity -= item.Quantity;

                    await _optionRepository.SaveChangesAsync();

                    existingProduct.Sold += item.Quantity;

                    await _productRepository.SaveChangesAsync();

                    var newOrderDetail = new OrderDetail() {
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

                if (!data.Voucher.IsNullOrEmpty()) {
                    var existingVoucher = await _voucherRepository.GetAll()
                        .FirstOrDefaultAsync(x => x.Code.Equals(data.Voucher), cancellationToken: cancellationToken) ?? throw new NotFoundException("Voucher not found!");

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

                await _mailService.SendOrderMail(data.Email, "Xác nhận đơn hàng", newOrder, orderDetails);

                await _orderRepository.CommitTransactionAsync();
            } catch (Exception e) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException(e.Message);
            }

            return new Response<string>(HttpStatusCode.Created, "Product created Successfully!");
        }
    }
}