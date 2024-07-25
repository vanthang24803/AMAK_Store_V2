using System.IdentityModel.Tokens.Jwt;
using AMAK.Application.Common.Constants;
using AMAK.Application.Services.Authentication;
using AMAK.Application.Services.Authentication.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class AuthenticationController : BaseController {
        private readonly IAuthService _authService;



        public AuthenticationController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
            return Ok(await _authService.RegisterAsync(request));
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            return Ok(await _authService.LoginAsync(request));
        }

        [HttpPost]
        [Route("RefreshToken")]
        [AllowAnonymous]

        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest request) {
            return Ok(await _authService.RefreshTokenAsync(request));
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]

        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request) {
            return Ok(await _authService.ForgotPasswordAsync(request));
        }

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassword([FromQuery] string userId, [FromQuery] string token, [FromBody] ResetPasswordRequest request) {
            return Ok(await _authService.ResetPasswordAsync(userId, token, request));
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout() {
            return Ok(await _authService.LogoutAsync(HttpContext.User));
        }

        [HttpGet]
        [Route("VerifyAccount")]
        [AllowAnonymous]

        public async Task<IActionResult> VerifyAccount([FromQuery] string userId, [FromQuery] string token) {
            return Ok(await _authService.VerifyAccountAsync(userId, token));
        }

        // TODO: Roles

        [HttpGet]
        [Route("Role/Seeds")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateSeeds() {
            return Ok(await _authService.CreateSeedRole());
        }

        [HttpGet]
        [Route("Role")]
        public async Task<IActionResult> GetAll() {
            return Ok(await _authService.GetRoles());
        }

        [HttpPost]
        [Route("Upgrade/Manager")]
        [Authorize(Roles = $"{Role.ADMIN}")]
        public async Task<IActionResult> UpgradeManager([FromBody] UpgradeRole upgradeRole) {
            return Ok(await _authService.UpgradeToManager(upgradeRole));
        }

        [HttpPost]
        [Route("Upgrade/Admin")]
        [Authorize(Roles = $"{Role.ADMIN}")]
        public async Task<IActionResult> UpgradeAdmin([FromBody] UpgradeRole upgradeRole) {
            return Ok(await _authService.UpgradeToAdmin(upgradeRole));
        }

        [HttpPost]
        [Route("Google")]
        [AllowAnonymous]

        public async Task<IActionResult> LoginWithGoogle([FromBody] SocialLoginRequest social) {
            return Ok(await _authService.SignInWithGoogle(social));
        }
    }
}