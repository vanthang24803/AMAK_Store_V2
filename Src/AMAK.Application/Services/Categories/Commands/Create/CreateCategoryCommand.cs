using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Category.Dtos;
using MediatR;

namespace AMAK.Application.Services.Categories.Commands.Create {
    public class CreateCategoryCommand : IRequest<Response<CategoryResponse>> {
        public CategoryRequest Category { get; set; } = null!;

        public CreateCategoryCommand(CategoryRequest category) {
            Category = category;
        }
    }
}