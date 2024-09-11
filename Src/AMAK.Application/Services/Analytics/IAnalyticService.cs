using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Analytics.Dtos;

namespace AMAK.Application.Services.Analytics {
    public interface IAnalyticService {
        Task<Response<BarChartResponse>> GetBarChartAsync();

        Task<Response<AreaChartResponse>> GetAreaChartAsync();

        Task<Response<StatisticalResponse>> GetStatisticalAsync(AnalyticQuery query);

        Task<Response<AnalyticCountResponse>> GetCountResponseAsync();

        Task<Response<List<AnalyticsUserResponse>>> GetAnalyticsUserAsync();
    }
}