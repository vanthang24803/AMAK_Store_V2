
using AMAK.Application.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Product.Commands.Thumbnail {
    public class UpdateThumbnailProductCommand(Guid productId, IFormFile thumbnail) : IRequest<Response<string>> {

        public Guid ProductId { get; set; } = productId;
        public IFormFile Thumbnail { get; set; } = thumbnail;
    }
}