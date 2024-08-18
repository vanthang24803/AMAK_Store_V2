
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;
using MediatR;

namespace AMAK.Application.Services.Product.Commands.Categories {
    public class UpdateProductCategoryCommand(Guid productId, UpdateProductCategoryRequest data) : IRequest<Response<string>> {
        public Guid ProductId { get; set; } = productId;

        public UpdateProductCategoryRequest Data { get; set; } = data;
    }
}