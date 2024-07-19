using AMAK.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Category {
    public class CategoryService : ICategoryService {

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        public CategoryService(IRepository<Domain.Models.Category> categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Domain.Models.Category>> GetAllAsync(string? search) {

            var query = _categoryRepository.GetAll();

            if (!string.IsNullOrEmpty(search)) {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<string> SaveAsync(string name) {

            var newCategory = new Domain.Models.Category() { Id = Guid.NewGuid(), Name = name };

            _categoryRepository.Add(newCategory);

            await _categoryRepository.SaveChangesAsync();

            return $"Category created Name is ${newCategory.Name}";
        }
    }
}