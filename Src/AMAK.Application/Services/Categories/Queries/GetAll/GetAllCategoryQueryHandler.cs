using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Category.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Categories.Queries.GetAll {
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, Response<List<CategoryResponse>>> {
        private readonly IRepository<Domain.Models.Category> _categoryRepository;

        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IRepository<Domain.Models.Category> categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken) {
            var categories = await _categoryRepository
                                .GetAll()
                                .Where(x => !x.IsDeleted)
                                .OrderByDescending(x => x.CreateAt)
                                .ToListAsync(cancellationToken);

            return new Response<List<CategoryResponse>>(HttpStatusCode.OK, _mapper.Map<List<CategoryResponse>>(categories));
        }
    }
}