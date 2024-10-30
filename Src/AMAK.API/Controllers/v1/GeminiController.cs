
using AMAK.Application.Common.Constants;
using AMAK.Application.Providers.Gemini;
using AMAK.Application.Providers.Gemini.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.Admin}, {Role.Manager}")]
    public class GeminiController : BaseController {
        private readonly IGeminiService _geminiService;

        public GeminiController(IGeminiService geminiService) {
            _geminiService = geminiService;
        }

        [HttpPost]
        [Route("Revenue")]

        public async Task<IActionResult> GenerateRevenueAI([FromBody] AiRequest<BarChartResponse> request) {
            return Ok(await _geminiService.GenerateRevenueAnalytic(request));
        }


        [HttpPost]
        [Route("Review")]
        [AllowAnonymous]

        public async Task<IActionResult> GetActionReviewAI([FromBody] AiRequest<List<ReviewResponse>> request) {
            return Ok(await _geminiService.GenerateReviewAnalytic(request));

        }

        [HttpPost]
        [Route("Statistic")]

        public async Task<IActionResult> GetActionStatisticAI([FromBody] AiRequest<AnalyticStatisticsResponse> request) {
            return Ok(await _geminiService.GenerateStatisticsAnalytic(request));

        }

        [HttpPost]
        [Authorize]
        [Route("Chat")]

        public async Task<IActionResult> AskWithAI([FromBody] GeminiChatRequest request) {
            return Ok(await _geminiService.AskWithAI(request, User));
        }

        [HttpGet]
        [Authorize]
        [Route("Chat")]

        public async Task<IActionResult> GetChatWithAI() {
            return Ok(await _geminiService.GetChatWithAI(User));
        }
    }
}