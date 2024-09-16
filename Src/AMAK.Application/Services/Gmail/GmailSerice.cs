using AMAK.Application.Common.Helpers;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Providers.Google;
using AMAK.Application.Services.Gmail.Dtos;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.Net;
using System.Text.RegularExpressions;

namespace AMAK.Application.Services.Gmail {
    public class GmailStoreService : IGmailStoreService {
        private readonly IGoogleService _googleService;
        private readonly ICacheService _cacheService;

        public GmailStoreService(IGoogleService googleService, ICacheService cacheService) {
            _googleService = googleService;
            _cacheService = cacheService;
        }

        public async Task<Response<List<EmailSummary>>> GetEmailsByGmailAsync() {
            var cacheKey = $"Gmail";

            var cachedData = await _cacheService.GetData<Response<List<EmailSummary>>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }

            var credential = await _googleService.LoginAsync();
            var emailSummaries = new List<EmailSummary>();

            using (var gmailService = new GmailService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential
            })) {
                var listRequest = gmailService.Users.Messages.List("me");
                listRequest.LabelIds = "INBOX";
                listRequest.MaxResults = 10;

                var listResponse = await listRequest.ExecuteAsync();

                if (listResponse.Messages.Count > 0) {
                    foreach (var message in listResponse.Messages) {
                        var getRequest = gmailService.Users.Messages.Get("me", message.Id);
                        var email = await getRequest.ExecuteAsync();

                        var emailSummary = ExtractEmailSummary(email, message.Id);
                        emailSummaries.Add(emailSummary);

                        emailSummaries = [.. emailSummaries.OrderByDescending(e => e.SentDate)];
                    }
                }
            }

            var result = new Response<List<EmailSummary>>(HttpStatusCode.OK, emailSummaries);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(10));

            return result;
        }


        public static EmailSummary ExtractEmailSummary(Message email, string messageId) {
            var headers = email.Payload?.Headers;
            var fromHeader = headers?.FirstOrDefault(h => h.Name == "From")?.Value;
            var subjectHeader = headers?.FirstOrDefault(h => h.Name == "Subject")?.Value;
            var dateHeader = headers?.FirstOrDefault(h => h.Name == "Date")?.Value;

            var emailAddress = ParseEmailAddress(fromHeader ?? "");
            var emailName = ParseEmailName(fromHeader ?? "");
            var sentDate = ParseDate(dateHeader);

            var isRead = email.LabelIds != null && email.LabelIds.Contains("UNREAD");

            return new EmailSummary {
                Id = messageId,
                Email = emailAddress,
                Subject = subjectHeader ?? "",
                Name = emailName,
                SentDate = sentDate,
                IsRead = !isRead
            };
        }

        private static string ParseEmailAddress(string fromHeader) {
            var match = Regex.Match(fromHeader, @"<(.*?)>");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        private static string ParseEmailName(string fromHeader) {
            var match = Regex.Match(fromHeader, @"^(.*?)(?=<)");
            return match.Success ? match.Groups[1].Value.Trim() : string.Empty;
        }

        private static DateTime ParseDate(string? dateHeader) {
            if (string.IsNullOrEmpty(dateHeader))
                return DateTime.MinValue;

            if (DateTime.TryParse(dateHeader, out var date))
                return date;

            return DateTime.MinValue;
        }
    }
}