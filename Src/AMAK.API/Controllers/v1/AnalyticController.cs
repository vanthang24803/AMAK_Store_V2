using Asp.Versioning;
using AMAK.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using AMAK.Application.Services.Analytics;
using Microsoft.AspNetCore.Mvc;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Gmail;
using Nest;
using AMAK.Application.Services.Search;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize(Roles = $"{Role.ADMIN}, {Role.MANAGER}")]
    public class AnalyticController : BaseController {
        private readonly IAnalyticService _analyticService;

        private readonly IGmailStoreService _gmailStoreService;

        private readonly ISearchService _searchService;

        public AnalyticController(IAnalyticService analyticService, IGmailStoreService gmailStoreService, ISearchService searchService) {
            _analyticService = analyticService;
            _gmailStoreService = gmailStoreService;
            _searchService = searchService;
        }

        [HttpGet]
        [Route("BarChart")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetStatistic() {
            return Ok(await _analyticService.GetAnalyticStatisticsAsync());
        }

        [HttpGet]
        [Route("TopProduct")]
        public async Task<IActionResult> GetTopProducts() {
            return Ok(await _analyticService.GetAnalyticTopProductsAsync());
        }

        [HttpGet]
        [Route("TopCustomer")]
        public async Task<IActionResult> GetTopCustomers() {
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

        [HttpGet]
        [Route("Accounts/{id}")]
        public async Task<IActionResult> GetAccountDetail([FromRoute] string id) {
            return Ok(await _analyticService.GetAccountDetail(id));
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("Gmail")]

        public async Task<IActionResult> GetGmail() {
            return Ok(await _gmailStoreService.GetEmailsByGmailAsync());
        }

        [HttpGet]
        [Route("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] SearchQuery searchQuery) {
            return Ok(await _searchService.Search(searchQuery));
        }


    }
}