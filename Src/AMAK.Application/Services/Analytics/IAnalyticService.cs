using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Me.Dtos;
using Google.Apis.Gmail.v1.Data;

namespace AMAK.Application.Services.Analytics {
    public interface IAnalyticService {
        Task<Response<BarChartResponse>> GetBarChartAsync();

        Task<Response<AreaChartResponse>> GetAreaChartAsync();

        Task<Response<List<PieChartResponse>>> GetPieChartAsync();

        Task<Response<AnalyticStatisticsResponse>> GetAnalyticStatisticsAsync();

        Task<Response<StatisticalResponse>> GetStatisticalAsync(AnalyticQuery query);

        Task<Response<AnalyticCountResponse>> GetCountResponseAsync();

        Task<Response<List<AnalyticsUserResponse>>> GetAnalyticsUserAsync();

        Task<Response<AnalyticTopProductResponse>> GetAnalyticTopProductsAsync();

        Task<Response<AnalyticTopCustomerResponse>> GetAnalyticTopCustomerAsync();

        Task<Response<ProfileResponse>> GetAccountDetail(string id);
    }
}