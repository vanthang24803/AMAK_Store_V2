using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Product.Dtos;

namespace AMAK.Application.Services.Product {
    public class ProductService : IProductService {

        private readonly IRepository<Domain.Models.Product> _productsRepository;

        private readonly IRepository<Domain.Models.Option> _optionRepository;

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        public ProductService(IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.Option> optionRepository, IRepository<Domain.Models.Category> categoryRepository) {
            _productsRepository = productRepository;
            _categoryRepository = categoryRepository;
            _optionRepository = optionRepository;
        }


        public Task<Response<ProductResponse>> CreateAsync(CreateProductRequest request) {
            throw new NotImplementedException();
        }

        public Task<Response<string>> DeleteAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<PaginationResponse<List<ProductResponse>>> GetAllAsync(ProductQuery query) {
            throw new NotImplementedException();
        }

        public Task<Response<ProductDetailResponse>> GetAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<Response<ProductResponse>> UpdateAsync(Guid id, UpdateProductRequest request) {
            throw new NotImplementedException();
        }
    }
}