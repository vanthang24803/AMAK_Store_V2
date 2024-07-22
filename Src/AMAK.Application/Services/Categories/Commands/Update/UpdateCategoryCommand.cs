using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Category.Dtos;
using MediatR;

namespace AMAK.Application.Services.Categories.Commands.Update {
    public class UpdateCategoryCommand : IRequest<Response<CategoryResponse>> {
        public Guid Id { get; set; }

        public CategoryRequest Category { get; set; } = null!;

        public UpdateCategoryCommand(Guid guid, CategoryRequest category) {
            Id = guid;
            Category = category;
        }

    }
}