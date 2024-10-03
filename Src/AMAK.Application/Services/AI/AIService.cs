using System.Net.Http.Json;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Services.AI.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;
using Microsoft.Extensions.Configuration;

namespace AMAK.Application.Services.AI {
    public class AIService : IAIService {

        private readonly HttpClient _httpClient;

        public AIService(HttpClient httpClient, IConfiguration configuration) {
            _httpClient = httpClient;

            var aiUrl = configuration["AISettings:Url"];

            if (string.IsNullOrEmpty(aiUrl)) {
                throw new ArgumentNullException(nameof(aiUrl), "AISettings URL cannot be null or empty.");
            }

            _httpClient.BaseAddress = new Uri(aiUrl);
        }

        public async Task<AIResponse> GenerateRevenueAnalytic(AIRequest<BarChartResponse> request) {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/analytic/chart", request);

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadFromJsonAsync<AIResponse>() ?? throw new NotFoundException("AI No Response!");
            return message;
        }

        public async Task<AIResponse> GenerateReviewAnalytic(AIRequest<List<ReviewResponse>> request) {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/analytic/review", request);

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadFromJsonAsync<AIResponse>() ?? throw new NotFoundException("AI No Response!");
            return message;
        }

        public async Task<AIResponse> GenerateStatisticsAnalytic(AIRequest<AnalyticStatisticsResponse> request) {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/analytic/statistic", request);

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadFromJsonAsync<AIResponse>() ?? throw new NotFoundException("AI No Response!");
            return message;
        }
    }
}
