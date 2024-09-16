using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Gmail.Dtos;

namespace AMAK.Application.Services.Gmail {
    public interface IGmailStoreService {
        Task<Response<List<EmailSummary>>> GetEmailsByGmailAsync();
    }
}