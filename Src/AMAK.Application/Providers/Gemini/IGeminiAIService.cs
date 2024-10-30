using AMAK.Application.Providers.Gemini.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;

namespace AMAK.Application.Providers.Gemini {
    public interface IGeminiService {
        Task<AiResponse> GenerateRevenueAnalytic(AiRequest<BarChartResponse> request);
        Task<AiResponse> GenerateReviewAnalytic(AiRequest<List<ReviewResponse>> request);
        Task<AiResponse> GenerateStatisticsAnalytic(AiRequest<AnalyticStatisticsResponse> request);
    }
}