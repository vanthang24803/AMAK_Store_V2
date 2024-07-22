using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Product.Commands.Create {
    public class CreateProductCommand : IRequest<Response<ProductResponse>> {
        public CreateProductRequest Product { get; set; } = null!;

        public IFormFile File { get; set; }

        public CreateProductCommand(CreateProductRequest productRequest , IFormFile file) {
            Product = productRequest;
            File = file;
        }
    }
}