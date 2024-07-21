using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Dtos;

namespace AMAK.Application.Services.Product {
    public interface IProductService {
        Task<PaginationResponse<List<ProductResponse>>> GetAllAsync(ProductQuery query);

        Task<Response<ProductDetailResponse>> GetAsync(Guid id);

        Task<Response<ProductResponse>> CreateAsync(CreateProductRequest request);

        Task<Response<ProductResponse>> UpdateAsync(Guid id, UpdateProductRequest request);

        Task<Response<string>> DeleteAsync(Guid id);
    }
}