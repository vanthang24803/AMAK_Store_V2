using Asp.Versioning;
using AMAK.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using AMAK.Application.Services.Analytics;
using Microsoft.AspNetCore.Mvc;
using AMAK.Application.Common.Query;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class AnalyticController : BaseController {
        private readonly IAnalyticService _analyticService;

        public AnalyticController(IAnalyticService analyticService) {
            _analyticService = analyticService;
        }

        [HttpGet]
        [Route("BarChart")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportChart() {
            return Ok(await _analyticService.GetBarChartAsync());
        }

        [HttpGet]
        [Route("AreaChart")]
        [AllowAnonymous]
        public async Task<IActionResult> AreaChart() {
            return Ok(await _analyticService.GetAreaChartAsync());
        }


        [HttpGet]
        [Route("Statistical")]
        public async Task<IActionResult> ExportStatistical([FromQuery] AnalyticQuery query) {
            return Ok(await _analyticService.GetStatisticalAsync(query));
        }



        [HttpGet]
        [Route("Count")]
        [AllowAnonymous]

        public async Task<IActionResult> GetCountResponse() {
            return Ok(await _analyticService.GetCountResponseAsync());
        }


        [HttpGet]
        [Route("Accounts")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAnalyticsUser() {
            return Ok(await _analyticService.GetAnalyticsUserAsync());
        }

    }
}