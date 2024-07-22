using AMAK.Application.Common.Helpers;
using MediatR;

namespace AMAK.Application.Services.Categories.Commands.Delete {
    public class DeleteCategoryCommand : IRequest<Response<string>> {
        public Guid Id { get; set; }

        public DeleteCategoryCommand(Guid id) {
            Id = id;
        }
    }
}