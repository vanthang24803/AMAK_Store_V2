
using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Tickets;
using AMAK.Application.Services.Tickets.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class TicketsController : BaseController {

        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService) {
            _ticketService = ticketService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FindAll([FromQuery] BaseQuery query) {
            return Ok(await _ticketService.GetAllAsync(query));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> FindDetail([FromRoute] Guid id) {
            return Ok(await _ticketService.GetDetailAsync(id));

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketSchema ticket) {
            return StatusCode(StatusCodes.Status201Created, await _ticketService.CreateAsync(ticket));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TicketSchema ticket) {
            return Ok(await _ticketService.UpdateAsync(id, ticket));

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _ticketService.DeleteAsync(id));

        }

        [HttpGet]
        [Route("Find")]
        [AllowAnonymous]

        public async Task<IActionResult> FindTicketByCode([FromBody] FindTicketRequest request) {
            return Ok(await _ticketService.FindTicketByCodeAsync(request));
        }

    }
}