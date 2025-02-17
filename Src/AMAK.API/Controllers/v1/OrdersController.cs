using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Order.Commands.Cancellation;
using AMAK.Application.Services.Order.Commands.Cancellation.Dto;
using AMAK.Application.Services.Order.Commands.Create;
using AMAK.Application.Services.Order.Commands.Delete;
using AMAK.Application.Services.Order.Commands.Update;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Order.Queries.GetAllOrder;
using AMAK.Application.Services.Order.Queries.GetByUser;
using AMAK.Application.Services.Order.Queries.GetDetailOderByUser;
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
        [Route("Me/{orderId:guid}")]
        public async Task<IActionResult> GetMeOrderById([FromRoute] Guid orderId) {
            return Ok(await _mediator.Send(new GetDetailOderByUserQuery(orderId, User)));
        }

        [HttpGet]
        [Route("Analytic")]
        [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]

        public async Task<IActionResult> GetAllOrders([FromQuery] BaseQuery query) {
            return Ok(await _mediator.Send(new GetAllOrderQuery(query)));
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
        [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]

        public async Task<IActionResult> UpdateStatusByAdmin([FromRoute] Guid id, [FromBody] UpdateStatusOrderRequest request) {
            return Ok(await _mediator.Send(new UpdateOrderStatusCommand(id, request)));
        }

        [HttpPut]
        [Route("Cancel")]
        public async Task<IActionResult> Cancellation([FromBody] OrderCancelRequest request) {
            return Ok(await _mediator.Send(new CancellationOrderCommand(User, request)));
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new DeleteOrderAccountCommand(User, id)));
        }
    }
}