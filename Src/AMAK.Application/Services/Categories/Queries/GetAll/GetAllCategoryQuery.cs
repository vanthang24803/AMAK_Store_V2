using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Category.Dtos;
using MediatR;

namespace AMAK.Application.Services.Categories.Queries.GetAll
{
    public class GetAllCategoryQuery : IRequest<Response<List<CategoryResponse>>>
    {
        
    }
}