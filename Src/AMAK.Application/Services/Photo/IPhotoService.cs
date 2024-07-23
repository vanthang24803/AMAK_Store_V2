using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Photo.Dtos;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Photo {
    public interface IPhotoService {
        Task<Response<List<PhotoResponse>>> CreateAsync(Guid productId, List<IFormFile> files);

        Task<Response<List<PhotoResponse>>> GetAllAsync(Guid productId);

        Task<Response<string>> DeleteAsync(Guid productId, Guid id);
    }
}