using AMAK.Application.Common.Helpers;
using MediatR;

namespace AMAK.Application.Services.Product.Commands.Delete {
    public class DeleteProductCommand : IRequest<Response<string>> {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id) {
            Id = id;
        }
    }
}