using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Category.Dtos;
using MediatR;

namespace AMAK.Application.Services.Categories.Queries.GetById {
    public class GetCategoryDetailQuery(Guid id) : IRequest<Response<CategoryResponse>> {
        public Guid Id { get; set; } = id;
    }
}