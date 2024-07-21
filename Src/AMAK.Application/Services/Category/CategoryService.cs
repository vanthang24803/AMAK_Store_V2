using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Category.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Category {
    public class CategoryService : ICategoryService {

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IMapper _mapper;

        public CategoryService(IRepository<Domain.Models.Category> categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> CreateAsync(CategoryRequest request) {
            var newCategory = _mapper.Map<Domain.Models.Category>(request);

            newCategory.Id = Guid.NewGuid();

            _categoryRepository.Add(newCategory);

            await _categoryRepository.SaveChangesAsync();

            return new Response<CategoryResponse>(HttpStatusCode.Created, _mapper.Map<CategoryResponse>(newCategory));
        }

        public async Task<Response<string>> DeleteAsync(Guid id) {
            var existCategory = await _categoryRepository.GetById(id) ?? throw new NotFoundException("Category not found!");

            if (existCategory.IsDeleted) {
                throw new NotFoundException("Category not found!");
            }

            existCategory.IsDeleted = true;

            await _categoryRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Category deleted successfully!");
        }

        public async Task<Response<List<CategoryResponse>>> GetAllAsync() {
            var categories = await _categoryRepository
                                .GetAll()
                                .Where(x => !x.IsDeleted)
                                .OrderByDescending(x => x.CreateAt)
                                .ToArrayAsync();

            return new Response<List<CategoryResponse>>(HttpStatusCode.OK, _mapper.Map<List<CategoryResponse>>(categories));
        }

        public async Task<Response<CategoryResponse>> GetAsync(Guid id) {
            var existCategory = await _categoryRepository.GetById(id) ?? throw new NotFoundException("Category not found!");

            if (existCategory.IsDeleted) {
                throw new NotFoundException("Category not found!");
            }

            return new Response<CategoryResponse>(HttpStatusCode.OK, _mapper.Map<CategoryResponse>(existCategory));
        }

        public async Task<Response<CategoryResponse>> UpdateAsync(Guid id, CategoryRequest request) {

            var existCategory = await _categoryRepository.GetById(id) ?? throw new NotFoundException("Category not found!");

            if (existCategory.IsDeleted) {
                throw new NotFoundException("Category not found!");
            }

            _mapper.Map(request, existCategory);

            _categoryRepository.Update(existCategory);

            await _categoryRepository.SaveChangesAsync();

            return new Response<CategoryResponse>(HttpStatusCode.OK, _mapper.Map<CategoryResponse>(existCategory));

        }
    }
}