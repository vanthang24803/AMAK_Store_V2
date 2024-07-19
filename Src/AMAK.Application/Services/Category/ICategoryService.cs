
namespace AMAK.Application.Services.Category {
    public interface ICategoryService {
        Task<string> SaveAsync(string name);

        Task<List<Domain.Models.Category>> GetAllAsync(string? search);
    }
}