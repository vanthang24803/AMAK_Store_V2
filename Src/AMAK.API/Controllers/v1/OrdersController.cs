using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Order.Commands.Cancellation;
using AMAK.Application.Services.Order.Commands.Create;
using AMAK.Application.Services.Order.Commands.Delete;
using AMAK.Application.Services.Order.Commands.Update;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Order.Queries.GetByUser;
using AMAK.Application.Services.Order.Queries.GetOrderById;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {

    [ApiVersion(1)]
    [Authorize]
    public class OrdersController : BaseController {

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator) {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CreateOrderRequest orderRequest) {
            return StatusCode(StatusCodes.Status201Created, await _mediator.Send(new CreateOrderCommand(User, orderRequest)));
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] OrderQuery query) {
            return Ok(await _mediator.Send(new GetAllOrderByAccountQuery(query, User)));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateOrderByAccountRequest request) {
            return Ok(await _mediator.Send(new UpdateOrderByAccountCommand(User, id, request)));
        }

        [HttpPut]
        [Route("{id:guid}/Status")]
        [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]

        public async Task<IActionResult> UpdateStatusByAdmin([FromRoute] Guid id, [FromBody] UpdateStatusOrderRequest request) {
            return Ok(await _mediator.Send(new UpdateOrderStatusCommand(id, request)));
        }

        [HttpDelete]
        [Route("{id:guid}/Cancel")]

        public async Task<IActionResult> Cancellation([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new CancellationOrderCommand(User, id)));
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new DeleteOrderAccountCommand(User, id)));
        }
    }
}