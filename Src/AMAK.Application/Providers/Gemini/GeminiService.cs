using System.Net.Http.Json;
using System.Text.Json;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Gemini.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Providers.Gemini {
    public class GeminiService : IGeminiService {
        private readonly string _gemini;
        private readonly HttpClient _httpClient;
        private readonly IRepository<Domain.Models.Prompt> _promptRepository;

        public GeminiService(HttpClient httpClient, IRepository<Domain.Models.Prompt> promptRepository, Configuration.IConfigurationProvider configurationProvider) {
            _httpClient = httpClient;
            _promptRepository = promptRepository;
            _gemini = InitializeGemini(configurationProvider).GetAwaiter().GetResult();
        }

        private static async Task<string> InitializeGemini(Configuration.IConfigurationProvider configurationProvider) {
            var cloudinarySettingsResponse = await configurationProvider.GetGeminiConfigAsync();
            var settings = cloudinarySettingsResponse.Result;

            var gemini = $"https://generativelanguage.googleapis.com/v1beta/models/{settings.Model}:generateContent?key={settings.ApiKey}";
            return gemini;

        }

        public async Task<AIResponse> GenerateRevenueAnalytic(AIRequest<BarChartResponse> request) {
            var jsonData = JsonSerializer.Serialize(request.Prompt);

            var prompt = await _promptRepository.GetAll()
                                                .FirstOrDefaultAsync(x => x.Type == Domain.Enums.EPrompt.ANALYTIC_REVENUE)
                                                ?? throw new BadRequestException("Prompt not found!");

            var context = prompt.Context;

            context = context.Replace("DATA", jsonData);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_gemini, ConvertGeminiRequest(context));

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadAsStringAsync() ?? throw new BadHttpRequestException("AI doesn't response");

            return ConvertGeminiResponse(message);
        }

        public async Task<AIResponse> GenerateReviewAnalytic(AIRequest<List<ReviewResponse>> request) {
            var jsonData = JsonSerializer.Serialize(request.Prompt);

            var prompt = await _promptRepository.GetAll()
                                                .FirstOrDefaultAsync(x => x.Type == Domain.Enums.EPrompt.ANALYTIC_REVIEW)
                                                ?? throw new BadRequestException("Prompt not found!");

            var context = prompt.Context;

            context = context.Replace("DATA", jsonData);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_gemini, ConvertGeminiRequest(context));

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadAsStringAsync() ?? throw new BadHttpRequestException("AI doesn't response");

            return ConvertGeminiResponse(message);
        }

        public async Task<AIResponse> GenerateStatisticsAnalytic(AIRequest<AnalyticStatisticsResponse> request) {
            var jsonData = JsonSerializer.Serialize(request.Prompt);

            var prompt = await _promptRepository.GetAll()
                                                .FirstOrDefaultAsync(x => x.Type == Domain.Enums.EPrompt.ANALYTIC_STATISTIC)
                                                ?? throw new BadRequestException("Prompt not found!");

            var context = prompt.Context;

            context = context.Replace("DATA", jsonData);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_gemini, ConvertGeminiRequest(context));

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadAsStringAsync() ?? throw new BadHttpRequestException("AI doesn't response");

            return ConvertGeminiResponse(message);
        }

        private static GeminiRequest.Root ConvertGeminiRequest(string prompt) {
            var request = new GeminiRequest.Root {
                Contents =
                [
                    new GeminiRequest.Content
                    {
                        Role = "user",
                        Parts =
                        [
                            new GeminiRequest.Part { Text = prompt }
                        ]
                    }
                ],
                GenerationConfig = new GeminiRequest.GenerationConfig {
                    Temperature = 1,
                    TopK = 64,
                    TopP = 0.95,
                    MaxOutputTokens = 8192,
                    ResponseMimeType = "text/plain"
                }
            };

            return request;
        }

        public static AIResponse ConvertGeminiResponse(string message) {
            try {
                GeminiResponse geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(message, new JsonSerializerOptions() {
                    PropertyNameCaseInsensitive = true,
                })
                ?? throw new BadRequestException("Convert Data Error!");

                if (geminiResponse != null && geminiResponse.Candidates != null) {
                    var result = new AIResponse() {
                        IsSuccess = true,
                        Code = 200,
                        Message = geminiResponse.Candidates[0].Content.Parts[0].Text,
                    };

                    return result;

                } else {
                    throw new BadRequestException("Deserialization failed.");
                }
            } catch (JsonException ex) {
                throw new BadRequestException($"Deserialization error: {ex.Message}");
            }
        }
    }
}
