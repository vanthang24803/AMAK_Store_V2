using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Providers.Cloudinary.Dtos;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Providers.Cloudinary {
    public interface ICloudinaryService {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string Id);
        Task<PaginationResponse<List<CloudinaryResponse>>> GetAllImages(BaseQuery query);
        Task<Response<string>> RemoveImage(RemoveCloudinaryRequest request);
        Task<Response<string>> UploadCloudImages(List<IFormFile> files);
    }
}