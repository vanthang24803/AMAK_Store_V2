using System;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Product.Commands.Update
{
    public class UpdateProductCommand : IRequest<Response<ProductResponse>>
    {
        public Guid Id {get; set;}

        public IFormFile? Thumbnail { get; set;}

        public UpdateProductRequest Product { get; set;} 

        public UpdateProductCommand(Guid guid, IFormFile? thumbnail, UpdateProductRequest productRequest)
        {
            Id = guid;
            Thumbnail = thumbnail;
            Product = productRequest;
        }

    }
}