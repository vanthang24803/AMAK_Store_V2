using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Options;
using AMAK.Application.Services.Options.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize($"{Role.ADMIN}, {Role.MANAGER}")]
    [Route("api/v{version:apiVersion}/Products")]

    public class OptionsController : BaseController {
        private readonly IOptionsService _optionsService;

        public OptionsController(IOptionsService optionsService) {
            _optionsService = optionsService;
        }

        [HttpGet]
        [Route("{productId}/[controller]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromRoute] Guid productId) {
            return Ok(await _optionsService.GetAllAsync(productId));
        }

        [HttpPost]
        [Route("{productId}/[controller]")]
        public async Task<IActionResult> Save([FromRoute] Guid productId, [FromBody] OptionRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _optionsService.CreateAsync(productId, request));
        }

        [HttpGet]
        [Route("{productId}/[controller]/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetail([FromRoute] Guid productId, [FromRoute] Guid id) {
            return Ok(await _optionsService.GetAsync(productId, id));
        }

        [HttpPost]
        [Route("{productId}/[controller]/{id}")]

        public async Task<IActionResult> Update([FromRoute] Guid productId, [FromRoute] Guid id, [FromBody] OptionRequest request) {
            return Ok(await _optionsService.UpdateAsync(productId, id, request));
        }

        [HttpDelete]
        [Route("{productId}/[controller]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productId, [FromRoute] Guid id) {
            return Ok(await _optionsService.DeleteAsync(productId, id));
        }
    }
}