using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Categories.Common;
using MediatR;

namespace AMAK.Application.Services.Categories.Commands.Create {
    public class CreateCategoryCommand(CategoryRequest category)
        : IRequest<Response<CategoryResponse>>
    {
        public CategoryRequest Category { get; set; } = category;
    }
}