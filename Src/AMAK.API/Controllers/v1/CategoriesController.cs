using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Categories.Commands.Create;
using AMAK.Application.Services.Categories.Commands.Delete;
using AMAK.Application.Services.Categories.Commands.Update;
using AMAK.Application.Services.Categories.Queries.GetAll;
using AMAK.Application.Services.Categories.Queries.GetById;
using AMAK.Application.Services.Categories.Common;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{StaticRole.ADMIN}, {StaticRole.MANAGER}")]
    public class CategoriesController : BaseController {

        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll() {
            var query = new GetAllCategoryQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id) {
            var query = new GetCategoryDetailQuery(id);
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CategoryRequest request) {
            var command = new CreateCategoryCommand(request);
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CategoryRequest request) {
            var command = new UpdateCategoryCommand(id, request);

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _mediator.Send(new DeleteCategoryCommand(id)));
        }
    }
}