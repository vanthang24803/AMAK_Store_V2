using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Analytics.Dtos;

namespace AMAK.Application.Services.Analytics {
    public interface IAnalyticService {
        Task<Response<Dictionary<string, double>>> GetBarChartAsync();

        Task<Response<StatisticalResponse>> GetStatisticalAsync(AnalyticQuery query);

        Task<Response<AnalyticCountResponse>> GetCountResponseAsync();
    }
}