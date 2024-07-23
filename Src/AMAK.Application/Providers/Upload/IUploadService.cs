using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Providers.Upload {
    public interface IUploadService {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string Id);
    }
}