using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Prompt;
using AMAK.Application.Services.Prompt.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]
    public class PromptsController : BaseController {

        private readonly IPromptService _promptService;

        public PromptsController(IPromptService promptService) {
            _promptService = promptService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll() {
            return Ok(await _promptService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] PromptRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _promptService.CreateAsync(request));
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> FindOne([FromRoute] Guid id) {
            return Ok(await _promptService.GetOneAsync(id));
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PromptRequest request) {
            return Ok(await _promptService.UpdateAsync(id, request));
        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Remove([FromRoute] Guid id) {
            return Ok(await _promptService.DeleteAsync(id));
        }
    }
}