using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Cart.Dtos;
using AMAK.Application.Services.Order.Dtos;

namespace AMAK.Application.Services.Cart {
    public interface ICartService {
        Task<Response<string>> AddToCartAsync(ClaimsPrincipal claims, CartRequest request);
        Task<Response<string>> RemoveToCartAsync(ClaimsPrincipal claims, CartRequest request);
        Task<Response<string>> ClearCartAsync(ClaimsPrincipal claims);
        Task<Response<string>> RemoveOptionsAsync(ClaimsPrincipal claims, CartRequest request);
        Task<Response<string>> HandlerBuyBack(ClaimsPrincipal claims, List<OrderDetailResponse> request);
        Task<Response<List<CartResponse>>> GetCartAsync(ClaimsPrincipal claims);
    }
}