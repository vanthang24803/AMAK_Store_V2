using AMAK.Application.Common.Constants;
using AMAK.Application.Providers.Configuration.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}")]
    public class ConfigurationController : BaseController {

        private readonly Application.Providers.Configuration.IConfigurationProvider _configurationProvider;

        public ConfigurationController(Application.Providers.Configuration.IConfigurationProvider configurationProvider) {
            _configurationProvider = configurationProvider;
        }

        [HttpGet]
        [Route("Mail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMailSettings() {
            return Ok(await _configurationProvider.GetEmailSettingAsync());
        }

        [HttpPost]
        [Route("Mail")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateMailSettings([FromBody] MailSettings mailSettings) {
            return Ok(await _configurationProvider.UpdateEmailSettingAsync(mailSettings));
        }

        [HttpGet]
        [Route("Cloudinary")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCloudinarySettings() {
            return Ok(await _configurationProvider.GetCloudinarySettingAsync());

        }

        [HttpPost]
        [Route("Cloudinary")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCloudinarySettings([FromBody] CloudinarySettings cloudinarySettings) {
            return Ok(await _configurationProvider.UpdateCloudinarySettingAsync(cloudinarySettings));

        }


        [HttpGet]
        [Route("Google")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGoogleSettings() {
            return Ok(await _configurationProvider.GetGoogleSettingAsync());

        }

        [HttpPost]
        [Route("Google")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateGoogleSettings([FromBody] GoogleSettings googleSettings) {
            return Ok(await _configurationProvider.UpdateGoogleSettingAsync(googleSettings));

        }


        [HttpGet]
        [Route("Momo")]
        [AllowAnonymous]

        public async Task<IActionResult> GetMomoSettings() {
            return Ok(await _configurationProvider.GetMomoSettingAsync());

        }

        [HttpPost]
        [Route("Momo")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateMomoSettings([FromBody] MomoSettings momoSettings) {
            return Ok(await _configurationProvider.UpdateMomoSettingAsync(momoSettings));

        }

        [HttpGet]
        [Route("Gemini")]
        [AllowAnonymous]

        public async Task<IActionResult> GetGeminiSettings() {
            return Ok(await _configurationProvider.GetGeminiConfigAsync());

        }

        [HttpPost]
        [Route("Gemini")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateGeminiSettings([FromBody] GeminiSettings geminiSettings) {
            return Ok(await _configurationProvider.UpdateGeminiConfig(geminiSettings));

        }
    }
}