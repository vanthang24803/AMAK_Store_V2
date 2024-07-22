using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Upload;
using AMAK.Application.Services.Category.Dtos;
using AMAK.Application.Services.Product.Common;
using AutoMapper;
using MediatR;
using System.Net;

namespace AMAK.Application.Services.Product.Commands.Create {
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<ProductResponse>> {

        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.Option> _optionRepository;
        private readonly IRepository<Domain.Models.Category> _categoryRepository;
        public readonly IRepository<Domain.Models.ProductCategory> _productCategoryRepository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(
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

        public async Task<Response<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken) {

            var newProduct = new Domain.Models.Product() {
                Id = Guid.NewGuid(),
                Name = request.Product.Name,
                Brand = request.Product.Brand,
                Introduction = request.Product.Introduction,
                Specifications = request.Product.Specifications,
            };

            var upload = await _uploadService.UploadPhotoAsync(request.File);

            if (upload.Error != null) {
                throw new BadRequestException(message: upload.Error.Message);
            }

            var categories = new List<Domain.Models.Category>();
            var options = new List<Domain.Models.Option>();

            newProduct.Thumbnail = upload.SecureUrl.AbsoluteUri;

            _productRepository.Add(newProduct);

            await _productRepository.SaveChangesAsync();

            foreach (var category in request.Product.Categories) {
                var existingCategory = await _categoryRepository.GetById(category.Id) ?? throw new NotFoundException("Category not found!");

                var productCategory = new Domain.Models.ProductCategory {
                    ProductId = newProduct.Id,
                    CategoryId = existingCategory.Id
                };
                _productCategoryRepository.Add(productCategory);
                categories.Add(existingCategory);
            }

            await _productCategoryRepository.SaveChangesAsync();

            foreach (var option in request.Product.Options) {
                var newOption = _mapper.Map<Domain.Models.Option>(option);
                newOption.Id = Guid.NewGuid();
                newOption.ProductId = newProduct.Id;

                _optionRepository.Add(newOption);
                options.Add(newOption);
            }

            await _optionRepository.SaveChangesAsync();


            return new Response<ProductResponse>(HttpStatusCode.Created, _mapper.Map<ProductResponse>(newProduct));
        }
    }
}