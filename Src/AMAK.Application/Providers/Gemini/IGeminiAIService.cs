using AMAK.Application.Providers.Gemini.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;

namespace AMAK.Application.Providers.Gemini {
    public interface IGeminiService {
        Task<AIResponse> GenerateRevenueAnalytic(AIRequest<BarChartResponse> request);
        Task<AIResponse> GenerateReviewAnalytic(AIRequest<List<ReviewResponse>> request);
        Task<AIResponse> GenerateStatisticsAnalytic(AIRequest<AnalyticStatisticsResponse> request);
    }
}