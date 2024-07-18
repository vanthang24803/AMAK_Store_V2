using AMAK.Application.Dtos.Auth;
using AMAK.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
            return Ok(await _authService.RegisterAsync(request));
        }
    }
}