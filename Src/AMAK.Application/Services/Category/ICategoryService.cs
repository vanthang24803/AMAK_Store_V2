using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Category.Dtos;

namespace AMAK.Application.Services.Category {
    public interface ICategoryService {
        Task<Response<List<CategoryResponse>>> GetAllAsync();

        Task<Response<CategoryResponse>> GetAsync(Guid id);

        Task<Response<CategoryResponse>> CreateAsync(CategoryRequest request);
        Task<Response<CategoryResponse>> UpdateAsync(Guid id, CategoryRequest request);

        Task<Response<string>> DeleteAsync(Guid id);
    }
}