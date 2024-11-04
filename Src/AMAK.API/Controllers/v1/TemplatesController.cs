using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Template;
using AMAK.Application.Services.Template.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {

    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]
    public class TemplatesController : BaseController {

        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService) {
            _templateService = templateService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTemplates() {
            return Ok(await _templateService.GetAll());
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save([FromBody] TemplateRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _templateService.CreateAsync(request));
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Put(Guid id, [FromBody] TemplateRequest request) {
            return Ok(await _templateService.UpdateAsync(id, request));
        }
    }
}