
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
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class GeminiController : BaseController {
        private readonly IGeminiService _geminiService;

        public GeminiController(IGeminiService geminiService) {
            _geminiService = geminiService;
        }

        [HttpPost]
        [Route("Revenue")]

        public async Task<IActionResult> GenerateRevenueAI([FromBody] AIRequest<BarChartResponse> request) {
            return Ok(await _geminiService.GenerateRevenueAnalytic(request));
        }


        [HttpPost]
        [Route("Review")]
        [AllowAnonymous]

        public async Task<IActionResult> GetActionReviewAI([FromBody] AIRequest<List<ReviewResponse>> request) {
            return Ok(await _geminiService.GenerateReviewAnalytic(request));

        }

        [HttpPost]
        [Route("Statistic")]

        public async Task<IActionResult> GetActionStatisticAI([FromBody] AIRequest<AnalyticStatisticsResponse> request) {
            return Ok(await _geminiService.GenerateStatisticsAnalytic(request));

        }
    }
}