using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;
using MediatR;

namespace AMAK.Application.Services.Product.Commands.Option {
    public class UpdateProductOptionCommand : IRequest<Response<string>> {
        public Guid ProductId { get; set; }
        public OptionsProductRequest Data { get; set; }

        public UpdateProductOptionCommand(Guid productId, OptionsProductRequest data) {
            ProductId = productId;
            Data = data;
        }
    }

    public class OptionsProductRequest {
        public List<OptionProductUpdateRequest> Options { get; set; } = [];
    }

}