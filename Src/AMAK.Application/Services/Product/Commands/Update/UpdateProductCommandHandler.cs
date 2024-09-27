using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Product.Common;
using AMAK.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Product.Commands.Update {

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductResponse>> {
        private readonly IRepository<Domain.Models.Product> _productRepository;
        public readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(
           IRepository<Domain.Models.Product> productRepository,
           IMapper mapper,
           IRepository<ProductCategory> productCategoryRepository,
           ICacheService cacheService
         ) {
            _mapper = mapper;
            _cacheService = cacheService;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        public async Task<Response<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {

            var cacheKey = $"GetDetailProduct_{request.Id}";

            await _productRepository.BeginTransactionAsync();

            try {
                var existingProduct = await _productRepository.GetAll()
                                                          .Where(x => !x.IsDeleted)
                                                          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Product not found");


                existingProduct.Name = request.Product.Name;
                existingProduct.Brand = request.Product.Brand;
                existingProduct.Introduction = request.Product.Introduction;
                existingProduct.Specifications = request.Product.Specifications;


                _productRepository.Update(existingProduct);
                await _productRepository.SaveChangesAsync();

                await _cacheService.RemoveData(cacheKey);

                await _productCategoryRepository.CommitTransactionAsync();

                return new Response<ProductResponse>(System.Net.HttpStatusCode.OK, _mapper.Map<ProductResponse>(existingProduct));

            } catch (Exception e) {
                await _productRepository.RollbackTransactionAsync();
                throw new BadRequestException(e.Message);
            }
        }

    }
}