using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Product.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Product.Queries.GetDetail {
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, Response<ProductDetailResponse>> {
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IMapper _mapper;

        public GetProductDetailQueryHandler(IRepository<Domain.Models.Product> productRepository, IMapper mapper) {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductDetailResponse>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken) {
            var existingProduct = await _productRepository.GetAll()
                                                          .Where(x => !x.IsDeleted)
                                                          .Include(c => c.Categories)
                                                          .Include(o => o.Options)
                                                          .Include(pt => pt.Photos)
                                                          .Include(r => r.Reviews)
                                                          .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Product not found!");

           


            return new Response<ProductDetailResponse>(HttpStatusCode.OK, _mapper.Map<ProductDetailResponse>(existingProduct));
        }
    }
}