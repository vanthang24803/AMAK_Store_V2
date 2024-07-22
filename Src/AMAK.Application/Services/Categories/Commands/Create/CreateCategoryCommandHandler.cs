using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Category.Dtos;
using AutoMapper;
using MediatR;
using System.Net;

namespace AMAK.Application.Services.Categories.Commands.Create {
    public class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, Response<CategoryResponse>> {

        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IRepository<Domain.Models.Category> categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
            var newCategory = _mapper.Map<Domain.Models.Category>(request.Category);

            newCategory.Id = Guid.NewGuid();

            _categoryRepository.Add(newCategory);

            await _categoryRepository.SaveChangesAsync();

            return new Response<CategoryResponse>(HttpStatusCode.Created, _mapper.Map<CategoryResponse>(newCategory));
        }
    }
}