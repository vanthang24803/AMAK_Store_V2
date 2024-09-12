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
        public async Task<IActionResult> BarChart() {
            return Ok(await _analyticService.GetBarChartAsync());
        }

        [HttpGet]
        [Route("AreaChart")]
        public async Task<IActionResult> AreaChart() {
            return Ok(await _analyticService.GetAreaChartAsync());
        }

        [HttpGet]
        [Route("PieChart")]
        public async Task<IActionResult> PieChart() {
            return Ok(await _analyticService.GetPieChartAsync());

        }

        [HttpGet]
        [Route("Statistic")]
        public async Task<IActionResult> GetStatistic() {
            return Ok(await _analyticService.GetAnalyticStatisticsAsync());
        }

        [HttpGet]
        [Route("TopProduct")]
        public async Task<IActionResult> GetTopProducts(){
            return Ok(await _analyticService.GetAnalyticTopProductsAsync());
        }

         [HttpGet]
        [Route("TopCustomer")]
        public async Task<IActionResult> GetTopCustomers(){
            return Ok(await _analyticService.GetAnalyticTopCustomerAsync());
        }


        [HttpGet]
        [Route("Statistical")]
        public async Task<IActionResult> ExportStatistical([FromQuery] AnalyticQuery query) {
            return Ok(await _analyticService.GetStatisticalAsync(query));
        }


        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> GetCountResponse() {
            return Ok(await _analyticService.GetCountResponseAsync());
        }


        [HttpGet]
        [Route("Accounts")]
        public async Task<IActionResult> GetAnalyticsUser() {
            return Ok(await _analyticService.GetAnalyticsUserAsync());
        }

    }
}