using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Upload;
using AMAK.Application.Services.Product.Common;
using AMAK.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;

namespace AMAK.Application.Services.Product.Commands.Update {

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductResponse>> {
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Option> _optionRepository;
        private readonly IRepository<Domain.Models.Category> _categoryRepository;
        public readonly IRepository<Domain.Models.ProductCategory> _productCategoryRepository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;


        public UpdateProductCommandHandler(
           IRepository<Domain.Models.Product> productRepository,
           IRepository<Domain.Models.Option> optionRepository,
           IRepository<Domain.Models.Category> categoryRepository,
           IUploadService uploadService,
           IMapper mapper,
           IRepository<Domain.Models.ProductCategory> productCategoryRepository) {
            _productRepository = productRepository;
            _optionRepository = optionRepository;
            _categoryRepository = categoryRepository;
            _uploadService = uploadService;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<Response<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {

            await _productRepository.BeginTransactionAsync();

            try {
                var existingProduct = await _productRepository.GetAll()
                                                          .Include(o => o.Options)
                                                          .Include(c => c.Categories)
                                                          .Where(x => !x.IsDeleted)
                                                          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Product not found");

                if (request.Thumbnail != null) {
                    var upload = await _uploadService.UploadPhotoAsync(request.Thumbnail);

                    if (upload.Error != null) {
                        throw new BadRequestException(message: upload.Error.Message);
                    }
                    existingProduct.Thumbnail = upload.SecureUrl.AbsoluteUri;
                }

                existingProduct.Name = request.Product.Name;
                existingProduct.Brand = request.Product.Brand;
                existingProduct.Introduction = request.Product.Introduction;
                existingProduct.Specifications = request.Product.Specifications;



                foreach (var option in request.Product.Options) {
                    if (option.Id == null) {
                        var newOption = _mapper.Map<Domain.Models.Option>(option);
                        newOption.Id = Guid.NewGuid();
                        newOption.ProductId = existingProduct.Id;
                        _optionRepository.Add(newOption);
                    } else {
                        var existOption = await _optionRepository.GetAll()
                                                                 .Where(x => x.Id == option.Id && !x.IsDeleted)
                                                                 .FirstOrDefaultAsync(cancellationToken)
                                        ?? throw new NotFoundException("Option not found");

                        if (option.IsDelete != null) {
                            _mapper.Map(option, existOption);
                            _optionRepository.Update(existOption);
                        } else {
                            existOption.IsDeleted = true;
                            _optionRepository.Update(existOption);
                        }
                    }
                }

                await _optionRepository.SaveChangesAsync();

                _productRepository.Update(existingProduct);
                await _productRepository.SaveChangesAsync();

                return new Response<ProductResponse>(System.Net.HttpStatusCode.OK, _mapper.Map<ProductResponse>(existingProduct));

            } catch (Exception e) {
                await _productRepository.RollbackTransactionAsync();
                throw new BadRequestException(e.Message);
            }
        }

    }
}