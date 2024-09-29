using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Providers.Upload;
using AMAK.Application.Services.Photo.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Photo {
    public class PhotoService : IPhotoService {

        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Models.Photo> _photoRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IUploadService _uploadService;
        private readonly ICacheService _cacheService;


        public PhotoService(IMapper mapper, IRepository<Domain.Models.Photo> photoRepository, IRepository<Domain.Models.Product> productRepository, IUploadService uploadService, ICacheService cacheService) {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _productRepository = productRepository;
            _uploadService = uploadService;
            _cacheService = cacheService;
        }

        public async Task<Response<List<PhotoResponse>>> CreateAsync(Guid productId, List<IFormFile> files) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingProduct = await _productRepository.GetById(productId) ?? throw new NotFoundException("Product not found!");

            var photos = new List<Domain.Models.Photo>();

            foreach (var file in files) {
                var upload = await _uploadService.UploadPhotoAsync(file);

                if (upload.Error != null) {
                    throw new BadRequestException(message: upload.Error.Message);
                }

                var newPhoto = new Domain.Models.Photo() {
                    Id = Guid.NewGuid(),
                    Url = upload.SecureUrl.AbsoluteUri,
                    PublicId = upload.PublicId,
                    ProductId = existingProduct.Id,
                };

                photos.Add(newPhoto);
            }

            _photoRepository.AddRange(photos);

            await _photoRepository.SaveChangesAsync();

            await _cacheService.RemoveData(cacheKey);

            return new Response<List<PhotoResponse>>(HttpStatusCode.Created, _mapper.Map<List<PhotoResponse>>(photos));
        }

        public async Task<Response<string>> DeleteAsync(Guid productId, Guid id) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingPhoto = await _photoRepository.GetAll()
                                .Include(x => x.Product)
                                .Where(x => !x.IsDeleted)
                                .FirstOrDefaultAsync(x => x.ProductId == productId)
            ?? throw new NotFoundException("Photo not found!");

            var deleted = await _uploadService.DeletePhotoAsync(existingPhoto.PublicId!);

            if (deleted.Error != null) {
                throw new BadRequestException(deleted.Error.Message);
            }

            _photoRepository.Remove(existingPhoto);

            await _photoRepository.SaveChangesAsync();

            await _cacheService.RemoveData(cacheKey);

            return new Response<string>(HttpStatusCode.OK, "Delete photo successfully!");
        }

        public async Task<Response<List<PhotoResponse>>> GetAllAsync(Guid productId) {
            var photos = await _photoRepository.GetAll()
                                .Where(x => x.ProductId == productId)
                                .Include(x => x.Product)
                                .Where(x => !x.IsDeleted)
                                .ToListAsync();

            return new Response<List<PhotoResponse>>(HttpStatusCode.OK, _mapper.Map<List<PhotoResponse>>(photos));
        }
    }
}