using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Providers.Cloudinary.Dtos;
using AMAK.Application.Providers.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace AMAK.Application.Providers.Cloudinary {
    public class CloudinaryService : ICloudinaryService {

        private readonly ILogger _logger;
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;
        private readonly ICacheService _cacheService;
        public CloudinaryService(IConfigurationProvider configurationProvider, ICacheService cacheService, ILogger<CloudinaryService> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheService = cacheService;
            _cloudinary = InitializeCloudinary(configurationProvider).GetAwaiter().GetResult();
        }


        private static async Task<CloudinaryDotNet.Cloudinary> InitializeCloudinary(IConfigurationProvider configurationProvider) {
            var cloudinarySettingsResponse = await configurationProvider.GetCloudinarySettingAsync();
            var settings = cloudinarySettingsResponse.Result;

            var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
            return new CloudinaryDotNet.Cloudinary(account);
        }


        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file) {
            var uploadResult = new ImageUploadResult();

            if (!ValidationFile(file)) return uploadResult;
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams {
                File = new FileDescription(file.Name, stream)
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId) {
            var deleteParams = new DeletionParams(publicId);

            var actions = await _cloudinary.DestroyAsync(deleteParams);

            return actions;
        }

        public async Task<Response<string>> RemoveImage(RemoveCloudinaryRequest request) {
            var tasks = request.Items.Select(async photo => {
                var deleteParams = new DeletionParams(photo.Id);
                var actions = await _cloudinary.DestroyAsync(deleteParams);

                if (actions.Error != null) {
                    throw new BadRequestException($"Remove Cloudinary Error: {actions.Error.Message}");
                }
            }).ToList();

            await Task.WhenAll(tasks);

            await _cacheService.RemoveData("Cloudinary_500");

            return new Response<string>(System.Net.HttpStatusCode.OK, "Remove Item Successfully!");
        }
        public async Task<Response<string>> UploadCloudImages(List<IFormFile> files) {
            var tasks = files.Select(async file => {
                if (!ValidationFile(file)) {
                    throw new BadRequestException($"Invalid file: {file.Name}");
                }

                var webpFile = this.ConvertExtensionWebpFile(file);

                await using var stream = webpFile.OpenReadStream();
                var uploadParams = new ImageUploadParams {
                    File = new FileDescription(webpFile.Name, stream)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null) {
                    throw new BadRequestException($"Upload Cloudinary Error: {uploadResult.Error.Message}");
                }

            }).ToList();

            await Task.WhenAll(tasks);

            await _cacheService.RemoveData("Cloudinary_500");

            return new Response<string>(System.Net.HttpStatusCode.OK, "Upload Cloudinary Successfully!");
        }

        public async Task<PaginationResponse<List<CloudinaryResponse>>> GetAllImages(BaseQuery query) {
            var cacheKey = $"Cloudinary_{query.Limit}";

            var cachedData = await _cacheService.GetData<PaginationResponse<List<CloudinaryResponse>>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }

            var listParams = new ListResourcesParams {
                ResourceType = ResourceType.Image,
                MaxResults = query.Limit,
                NextCursor = query.Page > 1 ? GetNextCursor(query.Limit) : null
            };

            var result = await _cloudinary.ListResourcesAsync(listParams);

            var imageUrls = result.Resources.Select(resource => new CloudinaryResponse() {
                PublicId = resource.PublicId,
                Format = resource.Format,
                Url = resource.Url,
                SecureUrl = resource.SecureUrl,
                Bytes = resource.Bytes,
                Width = resource.Width,
                Height = resource.Height,
                ResourceType = resource.ResourceType,
                CreatedAt = resource.CreatedAt,
            })
                .ToList();

            int totalItems = result.Resources.Length;

            var totalPages = (int)Math.Ceiling((double)totalItems / query.Limit);

            var response = new PaginationResponse<List<CloudinaryResponse>> {
                Code = 200,
                Status = "success",
                Result = imageUrls,
                CurrentPage = query.Page,
                TotalPage = totalPages,
                Items = query.Limit,
                TotalItems = totalItems
            };

            await _cacheService.SetData(cacheKey, response, DateTimeOffset.UtcNow.AddMinutes(10));

            return response;
        }


        private IFormFile ConvertExtensionWebpFile(IFormFile file) {
            try {
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                using var image = Image.Load(memoryStream);
                using var outputStream = new MemoryStream();
                image.Save(outputStream, new WebpEncoder());

                var webpFile = new FormFile(new MemoryStream(outputStream.ToArray()), 0, outputStream.Length, file.Name, $"{Path.GetFileNameWithoutExtension(file.FileName)}.webp") {
                    ContentType = "image/webp"
                };

                return webpFile;
            } catch (Exception e) {
                _logger.LogError("Convert file err {}", e.Message);
                throw new BadRequestException(e.Message);
            }
        }


        private static bool ValidationFile(IFormFile file) {
            if (file.Length <= 0) {
                throw new BadRequestException("File is required!");
            }

            List<string> validExtensions = [".png", ".jpg", ".webp", ".svg"];

            var extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension)) {
                throw new BadRequestException("File type is not valid! || .png , .jpg , .webp , .svg");
            }

            var size = file.Length;

            if (size > 5 * 1024 * 1024) {
                throw new BadRequestException("Max file size can be 5mb!");
            }

            return true;
        }

        private static string? GetNextCursor(int limit) {
            ArgumentOutOfRangeException.ThrowIfNegative(limit);
            return null;
        }

    }
}