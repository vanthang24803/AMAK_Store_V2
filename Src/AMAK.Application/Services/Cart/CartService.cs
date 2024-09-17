using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AMAK.Application.Services.Cart.Dtos;

namespace AMAK.Application.Services.Cart {
    public class CartService : ICartService {
        private readonly ICacheService _cacheService;
        private readonly IRepository<Domain.Models.Cart> _cartRepository;
        private readonly IRepository<CartDetail> _cartDetailRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public CartService(ICacheService cacheService, IRepository<Domain.Models.Cart> cartRepository, IRepository<Domain.Models.CartDetail> cartDetailRepository, UserManager<ApplicationUser> userManager) {
            _cacheService = cacheService;
            _cartRepository = cartRepository;
            _cartDetailRepository = cartDetailRepository;
            _userManager = userManager;
        }

        public async Task<Response<string>> AddToCartAsync(ClaimsPrincipal claims, CartRequest request) {
            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            var existingItem = await _cartDetailRepository.GetAll()
                .FirstOrDefaultAsync(x => x.OptionId == request.OptionId && x.CartId == existingUser.Id);

            if (existingItem != null) {
                existingItem.Quantity += 1;
                _cartDetailRepository.Update(existingItem);
            } else {
                var newCartItem = new CartDetail {
                    OptionId = request.OptionId,
                    OptionName = request.OptionName,
                    ProductId = request.ProductId,
                    ProductName = request.ProductName,
                    Price = request.Price,
                    Thumbnail = request.Thumbnail,
                    Sale = request.Sale,
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

            var cart = await _cartRepository.GetAll().Include(x => x.Details).FirstOrDefaultAsync(x => x.Id == existingUser.Id);


            if (cart == null) {
                cart = new Domain.Models.Cart {
                    Id = existingUser.Id,
                    UserId = existingUser.Id,
                    User = existingUser,
                };

                _cartRepository.Add(cart);
                await _cartRepository.SaveChangesAsync();
            }

            var cartResponse = cart.Details.Select(detail => new CartResponse {
                ProductId = detail.ProductId,
                ProductName = detail.ProductName,
                Thumbnail = detail.Thumbnail,
                OptionId = detail.OptionId,
                OptionName = detail.OptionName,
                Quantity = detail.Quantity,
                Price = detail.Price,
                Sale = detail.Sale,
            }).ToList();


            return new Response<List<CartResponse>>(HttpStatusCode.OK, cartResponse);
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