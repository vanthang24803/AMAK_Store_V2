using AMAK.Application.Common.Query;
using AMAK.Application.Services.Order;
using AMAK.Application.Services.Order.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {

    [ApiVersion(1)]
    [Authorize]
    public class OrdersController : BaseController {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CreateOrderRequest orderRequest) {
            return StatusCode(StatusCodes.Status201Created, await _orderService.CreateAsync(User, orderRequest));
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] BaseQuery query) {
            return Ok(await _orderService.GetByUser(User, query));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get([FromRoute] Guid id) {
            return Ok(await _orderService.GetAsync(id));
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _orderService.DeleteAsync(User, id));
        }
    }
}