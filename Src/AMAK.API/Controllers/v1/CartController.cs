using AMAK.Application.Services.Cart;
using AMAK.Application.Services.Cart.Dtos;
using AMAK.Application.Services.Order.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class CartController : BaseController {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) {
            _cartService = cartService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCart() {
            return Ok(await _cartService.GetCartAsync(User));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest request) {
            return Ok(await _cartService.AddToCartAsync(User, request));
        }

        [HttpPost]
        [Route("Remove")]

        public async Task<IActionResult> RemoveToCart([FromBody] CartRequest request) {
            return Ok(await _cartService.RemoveToCartAsync(User, request));
        }

        [HttpDelete]
        [Route("Clear")]

        public async Task<IActionResult> ClearCart() {
            return Ok(await _cartService.ClearCartAsync(User));
        }

        [HttpPost]
        [Route("BuyBack")]

        public async Task<IActionResult> BuyBack([FromBody] List<OrderDetailResponse> orders) {
            return Ok(await _cartService.HandlerBuyBack(User, orders));
        }

        [HttpPost]
        [Route("Remove/All")]
        public async Task<IActionResult> DeleteOptionCart([FromBody] CartRequest request) {
            return Ok(await _cartService.RemoveOptionsAsync(User, request));
        }
    }
}