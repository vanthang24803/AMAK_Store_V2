using AMAK.Application.Common.Constants;
using AMAK.Application.Services.FlashSale;
using AMAK.Application.Services.FlashSale.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]
    public class FlashSaleController : BaseController {
        private readonly IFlashSaleService _flashSaleService;
        public FlashSaleController(IFlashSaleService flashSaleService) {
            _flashSaleService = flashSaleService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save([FromBody] CreateFlashSaleRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _flashSaleService.CreateAsync(request));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FindAll() {
            return Ok(await _flashSaleService.FindAll());
        }

        [HttpGet]
        [Route("Sale")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSale() {
            return Ok(await _flashSaleService.GetFlashSaleAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindOne([FromRoute] Guid id) {
            return Ok(await _flashSaleService.FindOne(id));
        }

    }
}