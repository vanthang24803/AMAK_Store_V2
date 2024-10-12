using AMAK.Application.Common.Exceptions;
using AMAK.Application.Providers.Configuration.Dtos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AMAK.Application.Providers.Upload {
    public class UploadService : IUploadService {

        private readonly Cloudinary _cloudinary;

        private readonly CloudinarySettings _cloudinarySettings;

        public UploadService(IOptions<CloudinarySettings> options) {
            _cloudinarySettings = options.Value;

            var account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }


        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file) {
            var uploadResult = new ImageUploadResult();

            if (ValidationFile(file)) {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams {
                    File = new FileDescription(file.Name, stream)
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId) {
            var deleteParams = new DeletionParams(publicId);

            var actions = await _cloudinary.DestroyAsync(deleteParams);

            return actions;
        }


        private static bool ValidationFile(IFormFile file) {
            if (file.Length <= 0) {
                throw new BadRequestException("File is required!");
            }

            List<string> validExtensions = [".png", ".jpg", ".webp", ".svg"];

            string extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension)) {
                throw new BadRequestException("File type is not valid! || .png , .jpg , .webp , .svg");
            }

            long size = file.Length;

            if (size > 5 * 1024 * 1024) {
                throw new BadRequestException("Max file size can be 5mb!");
            }

            return true;
        }


    }
}