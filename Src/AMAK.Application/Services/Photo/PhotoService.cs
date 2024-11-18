using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Providers.Cloudinary;
using AMAK.Application.Providers.RabbitMq;
using AMAK.Application.Providers.RabbitMq.Common;
using AMAK.Application.Services.Photo.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AMAK.Application.Services.Photo {
    public class PhotoService : IPhotoService {

        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Models.Photo> _photoRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly ICloudinaryService _CloudinaryService;
        private readonly IRabbitProducer _rabbitProducer;
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;

        public PhotoService(IMapper mapper, IRepository<Domain.Models.Photo> photoRepository, IRepository<Domain.Models.Product> productRepository, ICloudinaryService CloudinaryService, ICacheService cacheService, ILogger<PhotoService> logger, IRabbitProducer rabbitProducer) {
            _mapper = mapper;
            _photoRepository = photoRepository;
            _productRepository = productRepository;
            _CloudinaryService = CloudinaryService;
            _cacheService = cacheService;
            _logger = logger;
            _rabbitProducer = rabbitProducer;
        }

        public async Task<Response<string>> CreateAsync(Guid productId, List<IFormFile> files) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingProduct = await _productRepository.GetById(productId) ?? throw new NotFoundException("Product not found!");

            var convertBase64 = new List<string>();

            foreach (var file in files) {
                var fileConvert = Util.ConvertImageToBase64(file);

                convertBase64.Add(fileConvert);
            }

            _rabbitProducer.SendMessage(RabbitQueue.Upload, new RabbitUpload() {
                ProductId = existingProduct.Id,
                Files = convertBase64
            });

            await _cacheService.RemoveData(cacheKey);

            return new Response<string>(HttpStatusCode.Created, "Upload photo successfully!");
        }

        public async Task<Response<string>> DeleteAsync(Guid productId, Guid id) {
            var cacheKey = $"GetDetailProduct_{productId}";

            var existingPhoto = await _photoRepository.GetAll()
                                .Include(x => x.Product)
                                .Where(x => !x.IsDeleted)
                                .FirstOrDefaultAsync(x => x.ProductId == productId)
            ?? throw new NotFoundException("Photo not found!");

            var deleted = await _CloudinaryService.DeletePhotoAsync(existingPhoto.PublicId!);

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