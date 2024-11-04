using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Categories.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Categories.Queries.GetAll {
    public class GetAllCategoryQueryHandler(
        IRepository<Domain.Models.Category> categoryRepository,
        IMapper mapper)
        : IRequestHandler<GetAllCategoryQuery, Response<List<CategoryResponse>>>
    {
        public async Task<Response<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken) {
            var categories = await categoryRepository
                                .GetAll()
                                .Where(x => !x.IsDeleted)
                                .OrderByDescending(x => x.CreateAt)
                                .ToListAsync(cancellationToken);

            return new Response<List<CategoryResponse>>(HttpStatusCode.OK, mapper.Map<List<CategoryResponse>>(categories));
        }
    }
}