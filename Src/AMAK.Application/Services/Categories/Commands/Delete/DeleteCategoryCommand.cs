using AMAK.Application.Common.Helpers;
using MediatR;

namespace AMAK.Application.Services.Categories.Commands.Delete {
    public class DeleteCategoryCommand(Guid id) : IRequest<Response<string>>
    {
        public Guid Id { get; set; } = id;
    }
}