using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Gemini.Dtos;
using AMAK.Application.Services.Analytics.Dtos;
using AMAK.Application.Services.Review.Dtos;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Providers.Gemini {
    public class GeminiService : IGeminiService {
        private readonly string _gemini;
        private readonly HttpClient _httpClient;
        private readonly IRepository<Prompt> _promptRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Conversation> _conversationRepository;


        public GeminiService(HttpClient httpClient, IRepository<Prompt> promptRepository, Configuration.IConfigurationProvider configurationProvider, UserManager<ApplicationUser> userManager, IRepository<Conversation> conversationRepository) {
            _httpClient = httpClient;
            _promptRepository = promptRepository;
            _gemini = InitializeGemini(configurationProvider).GetAwaiter().GetResult();
            _userManager = userManager;
            _conversationRepository = conversationRepository;
        }

        private static async Task<string> InitializeGemini(Configuration.IConfigurationProvider configurationProvider) {
            var cloudinarySettingsResponse = await configurationProvider.GetGeminiConfigAsync();
            var settings = cloudinarySettingsResponse.Result;

            var gemini = $"https://generativelanguage.googleapis.com/v1beta/models/{settings.Model}:generateContent?key={settings.ApiKey}";
            return gemini;

        }

        public async Task<AiResponse> GenerateRevenueAnalytic(AiRequest<BarChartResponse> request) {
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

        public async Task<AiResponse> GenerateReviewAnalytic(AiRequest<List<ReviewResponse>> request) {
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

        public async Task<AiResponse> GenerateStatisticsAnalytic(AiRequest<AnalyticStatisticsResponse> request) {
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

        public async Task<List<GeminiChatResponse>> AskWithAI(GeminiChatRequest request, ClaimsPrincipal claims) {
            var existingUser = await _userManager.GetUserAsync(claims)
                 ?? throw new NotFoundException("Account not found!");

            var prompt = Constants.Prompt.Chat.Replace("{DATA}", JsonSerializer.Serialize(request.Message));

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_gemini, ConvertGeminiRequest(prompt));

            if (!response.IsSuccessStatusCode) throw new BadRequestException("AI wrong!");

            var message = await response.Content.ReadAsStringAsync() ?? throw new BadHttpRequestException("AI doesn't response");

            var result = ConvertGeminiResponse(message);

            var newUserMessage = new Conversation() {
                Id = Guid.NewGuid(),
                Message = request.Message,
                IsBotReply = false,
                UserId = existingUser.Id
            };

            var newBotReplyMessage = new Conversation() {
                Id = Guid.NewGuid(),
                Message = result.Message,
                IsBotReply = true,
                UserId = existingUser.Id
            };

            _conversationRepository.AddRange([newUserMessage, newBotReplyMessage]);

            await _conversationRepository.SaveChangesAsync();


            return [
                new  GeminiChatResponse() {
                    Id = newUserMessage.Id,
                    IsBotReply= false,
                    Message= newUserMessage.Message,
                    CreateAt = newUserMessage.CreateAt,
                },
                new GeminiChatResponse(){
                    Id = newBotReplyMessage.Id,
                    Message = newBotReplyMessage.Message,
                    IsBotReply = true,
                    CreateAt = newBotReplyMessage.CreateAt,
                }
            ];
        }

        public async Task<Response<List<GeminiChatResponse>>> GetChatWithAI(ClaimsPrincipal claimsPrincipal) {

            var existingUser = await _userManager.GetUserAsync(claimsPrincipal)
            ?? throw new NotFoundException("Account not found!");

            var conversations = await _conversationRepository.GetAll()
            .Where(x => x.UserId == existingUser.Id)
            .Select(c => new GeminiChatResponse() {
                Id = c.Id,
                IsBotReply = c.IsBotReply,
                Message = c.Message,
                CreateAt = c.CreateAt
            })
            .OrderBy(x => x.CreateAt)
            .ToListAsync();

            return new Response<List<GeminiChatResponse>>(System.Net.HttpStatusCode.OK, conversations);
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

        private static AiResponse ConvertGeminiResponse(string message) {
            try {
                GeminiResponse geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(message, new JsonSerializerOptions() {
                    PropertyNameCaseInsensitive = true,
                })
                                                ?? throw new BadRequestException("Convert Data Error!");

                {
                    var result = new AiResponse() {
                        IsSuccess = true,
                        Code = 200,
                        Message = geminiResponse.Candidates[0].Content.Parts[0].Text,
                    };

                    return result;

                }
            } catch (JsonException ex) {
                throw new BadRequestException($"Deserialization error: {ex.Message}");
            }
        }


    }
}
