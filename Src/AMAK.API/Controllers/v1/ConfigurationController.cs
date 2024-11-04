using AMAK.Application.Common.Constants;
using AMAK.Application.Providers.Configuration.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}")]
    public class ConfigurationController : BaseController {

        private readonly Application.Providers.Configuration.IConfigurationProvider _configurationProvider;

        public ConfigurationController(Application.Providers.Configuration.IConfigurationProvider configurationProvider) {
            _configurationProvider = configurationProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetConfig() {
            return Ok(await _configurationProvider.GetAllConfig());
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> UpdateConfig([FromBody] Config config) {
            return Ok(await _configurationProvider.UpdateAllConfig(config));
        }

        [HttpGet]
        [Route("Mail")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMailSettings() {
            return Ok(await _configurationProvider.GetEmailSettingAsync());
        }

        [HttpPost]
        [Route("Mail")]
        public async Task<IActionResult> UpdateMailSettings([FromBody] MailSettings mailSettings) {
            return Ok(await _configurationProvider.UpdateEmailSettingAsync(mailSettings));
        }

        [HttpGet]
        [Route("Cloudinary")]
        public async Task<IActionResult> GetCloudinarySettings() {
            return Ok(await _configurationProvider.GetCloudinarySettingAsync());

        }

        [HttpPost]
        [Route("Cloudinary")]
        public async Task<IActionResult> UpdateCloudinarySettings([FromBody] CloudinarySettings cloudinarySettings) {
            return Ok(await _configurationProvider.UpdateCloudinarySettingAsync(cloudinarySettings));

        }


        [HttpGet]
        [Route("Google")]
        public async Task<IActionResult> GetGoogleSettings() {
            return Ok(await _configurationProvider.GetGoogleSettingAsync());

        }

        [HttpPost]
        [Route("Google")]
        public async Task<IActionResult> UpdateGoogleSettings([FromBody] GoogleSettings googleSettings) {
            return Ok(await _configurationProvider.UpdateGoogleSettingAsync(googleSettings));

        }


        [HttpGet]
        [Route("Momo")]
        public async Task<IActionResult> GetMomoSettings() {
            return Ok(await _configurationProvider.GetMomoSettingAsync());

        }

        [HttpPost]
        [Route("Momo")]
        public async Task<IActionResult> UpdateMomoSettings([FromBody] MomoSettings momoSettings) {
            return Ok(await _configurationProvider.UpdateMomoSettingAsync(momoSettings));

        }

        [HttpGet]
        [Route("Gemini")]
        public async Task<IActionResult> GetGeminiSettings() {
            return Ok(await _configurationProvider.GetGeminiConfigAsync());

        }

        [HttpPost]
        [Route("Gemini")]
        public async Task<IActionResult> UpdateGeminiSettings([FromBody] GeminiSettings geminiSettings) {
            return Ok(await _configurationProvider.UpdateGeminiConfig(geminiSettings));

        }
    }
}