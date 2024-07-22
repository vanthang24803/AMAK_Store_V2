using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Options.Dtos;

namespace AMAK.Application.Services.Options {
    public interface IOptionsService {
        Task<Response<List<OptionResponse>>> GetAllAsync(Guid productId);

        Task<Response<OptionResponse>> GetAsync(Guid productId, Guid id);

        Task<Response<OptionResponse>> CreateAsync(Guid productId, OptionRequest request);

        Task<Response<OptionResponse>> UpdateAsync(Guid id, Guid productId, OptionRequest request);

        Task<Response<string>> DeleteAsync(Guid productId, Guid id);

    }
}