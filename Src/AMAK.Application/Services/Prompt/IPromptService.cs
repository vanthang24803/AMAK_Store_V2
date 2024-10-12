using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Prompt.Dtos;

namespace AMAK.Application.Services.Prompt {
    public interface IPromptService {
        Task<Response<string>> CreateAsync(PromptRequest request);
        Task<Response<List<PromptResponse>>> GetAllAsync();
        Task<Response<PromptResponse>> GetOneAsync(Guid id);
        Task<Response<string>> UpdateAsync(Guid id, PromptRequest request);
        Task<Response<string>> DeleteAsync(Guid id);
    }
}