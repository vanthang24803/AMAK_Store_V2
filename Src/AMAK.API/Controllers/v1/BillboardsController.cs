using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Billboard;
using AMAK.Application.Services.Billboard.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]

    public class BillboardsController : BaseController {
        private readonly IBillboardService _billboardService;

        public BillboardsController(IBillboardService billboardService) {
            _billboardService = billboardService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll() {
            return Ok(await _billboardService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile billboard, [FromForm] CreateBillboardRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _billboardService.CreateAsync(billboard , request));

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            return Ok(await _billboardService.DeleteAsync(id));
        }
    }
}