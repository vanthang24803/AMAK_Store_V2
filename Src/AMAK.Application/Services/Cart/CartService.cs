using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AMAK.Application.Services.Cart.Dtos;
using AMAK.Application.Services.Order.Dtos;

namespace AMAK.Application.Services.Cart {
    public class CartService : ICartService {
        private readonly IRepository<Domain.Models.Cart> _cartRepository;
        private readonly IRepository<CartDetail> _cartDetailRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(IRepository<Domain.Models.Cart> cartRepository, IRepository<CartDetail> cartDetailRepository, UserManager<ApplicationUser> userManager) {
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _userManager = userManager;
        }

        public async Task<Response<string>> AddToCartAsync(ClaimsPrincipal claims, CartRequest request) {
            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            var existingItem = await _cartDetailRepository.GetAll()
                .Include(o => o.Option)
                .FirstOrDefaultAsync(x => x.OptionId == request.OptionId && x.CartId == existingUser.Id);

            if (existingItem != null) {
                existingItem.Quantity += 1;
                _cartDetailRepository.Update(existingItem);
            } else {
                var newCartItem = new CartDetail {
                    OptionId = request.OptionId,
                    Quantity = request.Quantity,
                    CartId = existingUser.Id
                };

                _cartDetailRepository.Add(newCartItem);
            }

            await _cartDetailRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Add to Cart Success!");
        }


        public async Task<Response<string>> ClearCartAsync(ClaimsPrincipal claims) {
            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            var cartDetails = await _cartDetailRepository.GetAll()
                .Where(x => x.CartId == existingUser.Id)
                .ToListAsync();

            if (cartDetails.Count != 0) {
                _cartDetailRepository.RemoveRange(cartDetails);
            }

            await _cartDetailRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Cleared all cart details successfully!");
        }

        public async Task<Response<List<CartResponse>>> GetCartAsync(ClaimsPrincipal claims) {
            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            var cartResponse = await _cartRepository.GetAll()
                .Where(x => x.UserId == existingUser.Id)
                .Select(cart => cart.Details.Select(detail => new CartResponse {
                    ProductId = detail.Option.ProductId,
                    ProductName = detail.Option.Product.Name,
                    Thumbnail = detail.Option.Product.Thumbnail ?? "",
                    OptionId = detail.OptionId,
                    OptionName = detail.Option.Name,
                    Quantity = detail.Quantity,
                    Price = detail.Option.Price,
                    Sale = detail.Option.IsFlashSale ? 50 : detail.Option.Sale,
                }).OrderBy(c => c.ProductName).ToList())
                .FirstOrDefaultAsync();

            if (cartResponse == null) {
                var newCart = new Domain.Models.Cart {
                    Id = existingUser.Id,
                    UserId = existingUser.Id,
                    User = existingUser,
                };

                _cartRepository.Add(newCart);
                await _cartRepository.SaveChangesAsync();

                cartResponse = [];
            }

            return new Response<List<CartResponse>>(HttpStatusCode.OK, cartResponse);
        }

        public async Task<Response<string>> HandlerBuyBack(ClaimsPrincipal claims, List<OrderDetailResponse> orders) {
            var existingUser = await _userManager.GetUserAsync(claims)
                 ?? throw new NotFoundException("Account not found!");

            var cart = await _cartRepository.GetAll()
                .FirstOrDefaultAsync(x => x.UserId == existingUser.Id)
                ?? throw new NotFoundException("Cart not found!");

            var cartItems = await _cartDetailRepository.GetAll()
                .Where(x => x.CartId == cart.Id)
                .ToListAsync();

            var cartItemsDict = cartItems.ToDictionary(x => x.OptionId, x => x);

            foreach (var order in orders) {
                var optionId = order.OptionId;
                if (cartItemsDict.TryGetValue(optionId, out var existingItem)) {
                    existingItem.Quantity += 1;
                    _cartDetailRepository.Update(existingItem);
                } else {
                    var newCartItem = new CartDetail() {
                        CartId = cart.Id,
                        OptionId = optionId,
                        Quantity = order.Quantity,
                    };

                    _cartDetailRepository.Add(newCartItem);
                }
            }

            await _cartDetailRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Add to Cart Success!");
        }

        public async Task<Response<string>> RemoveOptionsAsync(ClaimsPrincipal claims, CartRequest request) {
            var existingUser = await _userManager.GetUserAsync(claims)
                 ?? throw new NotFoundException("Account not found!");

            var existingItem = await _cartDetailRepository.GetAll()
                .FirstOrDefaultAsync(x => x.OptionId == request.OptionId && x.CartId == existingUser.Id)
                ?? throw new NotFoundException("Cart item not found!");

            _cartDetailRepository.Remove(existingItem);

            await _cartDetailRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Deleted cart success!");
        }

        public async Task<Response<string>> RemoveToCartAsync(ClaimsPrincipal claims, CartRequest request) {
            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            var existingItem = await _cartDetailRepository.GetAll()
                .FirstOrDefaultAsync(x => x.OptionId == request.OptionId && x.CartId == existingUser.Id)
                ?? throw new NotFoundException("Cart item not found!");

            existingItem.Quantity -= 1;

            if (existingItem.Quantity <= 0) {
                _cartDetailRepository.Remove(existingItem);
            } else {
                _cartDetailRepository.Update(existingItem);
            }

            await _cartDetailRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Deleted cart success!");
        }

    }
}