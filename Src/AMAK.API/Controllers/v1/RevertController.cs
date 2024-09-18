using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Revert;
using AMAK.Application.Services.Revert.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {

    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class RevertController : BaseController {

        private readonly IRevertService _revertService;

        public RevertController(IRevertService revertService) {
            _revertService = revertService;
        }

        [HttpPost]
        [Route("Product")]
        public async Task<IActionResult> RevertProducts([FromBody] ProductRevertRequest request) {
            return Ok(await _revertService.RevertProductAsync(request));
        }

        [HttpPost]
        [Route("Option")]
        public async Task<IActionResult> RevertOptions([FromBody] OptionRevertRequest request) {
            return Ok(await _revertService.RevertOptionAsync(request));
        }

        [HttpPost]
        [Route("Media")]

        public async Task<IActionResult> RevertMedia([FromBody] MediaRevertRequest request) {
            return Ok(await _revertService.RevertMediaAsync(request));

        }
    }
}