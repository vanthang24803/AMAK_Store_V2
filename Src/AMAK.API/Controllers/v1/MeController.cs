using AMAK.Application.Services.Me;
using AMAK.Application.Services.Me.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class MeController : BaseController {

        private readonly IMeService _meService;

        public MeController(IMeService meService) {
            _meService = meService;
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> GetProfile() {
            return Ok(await _meService.GetProfileAsync(HttpContext.User));
        }

        [HttpPut]
        [Route("Profile")]

        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request) {
            return Ok(await _meService.UpdateProfileAsync(HttpContext.User, request));
        }

        [HttpPost]
        [Route("Avatar")]

        public async Task<IActionResult> UploadAvatar(IFormFile avatar) {
            return StatusCode(StatusCodes.Status201Created, await _meService.UploadAvatarAsync(HttpContext.User, avatar));
        }

        [HttpPut]
        [Route("Password")]

        public async Task<IActionResult> UploadPassword([FromBody] UpdatePasswordRequest request) {
            return Ok(await _meService.UpdatePasswordAsync(HttpContext.User, request));
        }

    }
}