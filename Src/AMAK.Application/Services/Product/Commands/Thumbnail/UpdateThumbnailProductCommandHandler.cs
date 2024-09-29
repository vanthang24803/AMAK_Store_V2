using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Providers.Upload;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Product.Commands.Thumbnail {
    public class UpdateThumbnailProductCommandHandler : IRequestHandler<UpdateThumbnailProductCommand, Response<string>> {
        private readonly IUploadService _uploadService;
        private readonly ICacheService _cacheService;
        private readonly IRepository<Domain.Models.Product> _productRepository;

        public UpdateThumbnailProductCommandHandler(IRepository<Domain.Models.Product> productRepository, IUploadService uploadService, ICacheService cacheService) {
            _productRepository = productRepository;
            _uploadService = uploadService;
            _cacheService = cacheService;
        }

        public async Task<Response<string>> Handle(UpdateThumbnailProductCommand request, CancellationToken cancellationToken) {
            var cacheKey = $"GetDetailProduct_{request.ProductId}";

            try {
                await _productRepository.BeginTransactionAsync();
                var existingProduct = await _productRepository.GetAll()
                                       .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken)
                                       ?? throw new NotFoundException("Product not found!");

                var upload = await _uploadService.UploadPhotoAsync(request.Thumbnail);

                if (upload.Error != null) {
                    throw new BadRequestException(message: upload.Error.Message);
                }

                existingProduct.Thumbnail = upload.SecureUrl.AbsoluteUri;

                _productRepository.Update(existingProduct);

                await _productRepository.SaveChangesAsync();

                await _productRepository.CommitTransactionAsync();
            } catch (Exception e) {
                await _productRepository.RollbackTransactionAsync();
                throw new BadRequestException(e.Message);
            }

            await _cacheService.RemoveData(cacheKey);

            return new Response<string>(System.Net.HttpStatusCode.OK, "Updated Thumbnail Successfully!");
        }
    }
}