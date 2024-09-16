using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Cart.Dtos;

namespace AMAK.Application.Services.Cart
{
    public interface ICartService
    {
        Task<Response<string>> AddToCartAsync(ClaimsPrincipal claims , CartRequest request);
        Task<Response<string>> RemoveToCartAsync(ClaimsPrincipal claims , CartRequest request);
        Task<Response<string>> ClearCacheAsync(ClaimsPrincipal claims);
        Task<Response<List<CartResponse>>> GetCartAsync(ClaimsPrincipal claims);
    }
}