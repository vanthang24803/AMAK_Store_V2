
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Revert.Dtos;

namespace AMAK.Application.Services.Revert {
    public interface IRevertService {
        Task<Response<string>> RevertProductAsync(ProductRevertRequest request);
        Task<Response<string>> RevertOptionAsync(OptionRevertRequest request);
        Task<Response<string>> RevertMediaAsync(MediaRevertRequest request);
    }
}