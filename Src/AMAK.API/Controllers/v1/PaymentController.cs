using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Payment.Commands.Momo;
using AMAK.Application.Services.Payment.Dtos;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class PaymentController : BaseController {

        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Momo")]
        public async Task<IActionResult> Save([FromBody] CreateOrderRequest orderRequest) {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(new PaymentMomoCommand(User, orderRequest)));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Momo/Handler")]

        public async Task<IActionResult> HandlerMomo([FromBody] HandlerMomoRequest request) {
            return Ok(await _mediator.Send(new HandlerMomoRequestCommand(request)));
        }
    }
}