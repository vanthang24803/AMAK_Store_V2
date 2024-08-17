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
        [Route("Chart")]

        public async Task<IActionResult> ExportChart() {
            return Ok(await _analyticService.GetBarChartAsync());
        }


        [HttpGet]
        [Route("Statistical")]
        public async Task<IActionResult> ExportStatistical([FromQuery] AnalyticQuery query) {
            return Ok(await _analyticService.GetStatisticalAsync(query));
        }

    }
}