using AMAK.Application.Services.Authentication;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Validations;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiController]
    [ApiVersion(1)]
    [ValidateModelState]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AuthenticationController : ControllerBase {

        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
            return Ok(await _authService.RegisterAsync(request));
        }

        [HttpGet]
        [Route("Role/Seeds")]
        public async Task<IActionResult> CreateSeeds() {
            return Ok(await _authService.CreateSeedRole());
        }

        [HttpGet]
        [Route("Role")]
        public async Task<IActionResult> GetAll() {
            return Ok(await _authService.GetRoles());
        }
    }
}