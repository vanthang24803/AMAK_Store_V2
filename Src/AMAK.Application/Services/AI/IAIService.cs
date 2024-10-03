using AMAK.Application.Services.AI.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;

namespace AMAK.Application.Services.AI {
    public interface IAIService {
        Task<AIResponse> GenerateRevenueAnalytic(AIRequest<BarChartResponse> request);
        Task<AIResponse> GenerateReviewAnalytic(AIRequest<List<ReviewResponse>> request);
        Task<AIResponse> GenerateStatisticsAnalytic(AIRequest<AnalyticStatisticsResponse> request);
    }
}