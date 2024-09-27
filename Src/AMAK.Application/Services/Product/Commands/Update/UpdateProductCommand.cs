using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;
using MediatR;

namespace AMAK.Application.Services.Product.Commands.Update {
    public class UpdateProductCommand : IRequest<Response<ProductResponse>> {
        public Guid Id { get; set; }
        public UpdateProductRequest Product { get; set; }

        public UpdateProductCommand(Guid guid, UpdateProductRequest productRequest) {
            Id = guid;
            Product = productRequest;
        }

    }
}