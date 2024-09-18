using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Trash.Dtos;

namespace AMAK.Application.Services.Trash {
    public interface ITrashService {
        Task<Response<List<ProductDeletedResponse>>> GetProductTrashAsync();
        Task<Response<List<OptionDeletedResponse>>> GetOptionTrashAsync();
        Task<Response<List<PhotoDeletedResponse>>> GetPhotoResponse();
    }
}