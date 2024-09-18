using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Momo;
using AMAK.Domain.Enums;
using AMAK.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AMAK.Application.Services.Payment.Commands.Momo {
    public class PaymentMomoCommandHandler : IRequestHandler<PaymentMomoCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Order> _orderRepository;
        public readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Voucher> _voucherRepository;

        private readonly IRepository<Option> _optionRepository;

        private readonly IRepository<OrderDetail> _orderDetailRepository;

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly IMomoService _momoService;

        public PaymentMomoCommandHandler(IRepository<Domain.Models.Product> productRepository, IRepository<OrderDetail> orderDetailRepository, IRepository<Option> optionRepository, IRepository<Voucher> voucherRepository, UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Order> orderRepository, IMomoService momoService) {
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
            _optionRepository = optionRepository;
            _voucherRepository = voucherRepository;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _momoService = momoService;
        }



        public async Task<Response<string>> Handle(PaymentMomoCommand request, CancellationToken cancellationToken) {

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
                    Payment = EPayment.MOMO,
                    Quantity = data.Quantity,
                    Shipping = true,
                    NumberPhone = data.NumberPhone,
                    Status = [
                        new OrderStatus() {
                            OrderId = data.Id,
                            Status = EOrderStatus.PENDING,
                            TimeStamp = DateTime.UtcNow,
                        }
                    ],
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


                await _orderRepository.CommitTransactionAsync();
            } catch (Exception) {
                await _orderRepository.RollbackTransactionAsync();
                throw new BadRequestException("Query wrong!");
            }

            var response = await _momoService.CreateMomoPaymentAsync(data);

            return new Response<string>(HttpStatusCode.Created, response);

        }
    }
}