using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Product.Commands.Categories {
    public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IRepository<Domain.Models.ProductCategory> _productCategoryRepository;

        private readonly ICacheService _cacheService;

        public UpdateProductCategoryCommandHandler(IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.Category> categoryRepository, IRepository<Domain.Models.ProductCategory> productCategoryRepository, ICacheService cacheService) {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
            _cacheService = cacheService;
        }

        public async Task<Response<string>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken) {

            var cacheKey = $"GetDetailProduct_{request.ProductId}";

            var existingProduct = await _productRepository.GetById(request.ProductId) ?? throw new NotFoundException("Product not found!");

            var productCategories = await _productCategoryRepository.GetAll()
                                                                    .Where(x => x.ProductId == request.ProductId)
                                                                    .ToListAsync(cancellationToken: cancellationToken);
            try {
                await _productCategoryRepository.BeginTransactionAsync();

                _productCategoryRepository.RemoveRange(productCategories);

                await _productCategoryRepository.SaveChangesAsync();

                List<Domain.Models.ProductCategory> addNewProductCategories = [];

                foreach (var category in request.Data.Categories) {
                    var existingCategory = await _categoryRepository.GetById(category.Id) ?? throw new NotFoundException("Product not found!");
                    var newProductCategory = new Domain.Models.ProductCategory {
                        CategoryId = existingCategory.Id,
                        ProductId = existingProduct.Id,
                    };
                    addNewProductCategories.Add(newProductCategory);
                }

                _productCategoryRepository.AddRange(addNewProductCategories);

                await _productCategoryRepository.SaveChangesAsync();

                await _productCategoryRepository.CommitTransactionAsync();
            } catch (Exception) {
                await _productCategoryRepository.RollbackTransactionAsync();
                throw new BadRequestException("Wrong queries!");
            }

            await _cacheService.RemoveData(cacheKey);

            return new Response<string>(System.Net.HttpStatusCode.OK, "Updated categories successfully!");
        }
    }
}