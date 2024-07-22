using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Category.Dtos;
using AutoMapper;
using MediatR;

namespace AMAK.Application.Services.Categories.Queries.GetById {
    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, Response<CategoryResponse>> {
        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IMapper _mapper;

        public GetCategoryDetailQueryHandler(IRepository<Domain.Models.Category> categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken) {
            var existCategory = await _categoryRepository.GetById(request.Id) ?? throw new NotFoundException("Category not found!");

            if (existCategory.IsDeleted) {
                throw new NotFoundException("Category not found!");
            }

            return new Response<CategoryResponse>(HttpStatusCode.OK, _mapper.Map<CategoryResponse>(existCategory));
        }
    }
}