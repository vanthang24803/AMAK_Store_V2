
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Product.Commands.Delete {
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Product> _productRepository;

        private readonly ICacheService _cacheService;

        public DeleteProductCommandHandler(IRepository<Domain.Models.Product> productRepository, ICacheService cacheService) {
            _productRepository = productRepository;
            _cacheService = cacheService;
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
            var cacheKey = $"GetDetailProduct_{request.Id}";

            var existingProduct = await _productRepository.GetAll()
                                                            .Include(x => x.Options)
                                                          .Where(x => x.Id == request.Id)
                                                          .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException("Product not found!");

            if (!existingProduct.IsDeleted) {
                existingProduct.IsDeleted = true;
                existingProduct.DeleteAt = DateTime.UtcNow;


                foreach (var option in existingProduct.Options) {
                    option.IsDeleted = true;
                    option.DeleteAt = DateTime.UtcNow;
                }

                await _productRepository.SaveChangesAsync();
            } else {

                _productRepository.Remove(existingProduct);

                await _productRepository.SaveChangesAsync();
            }

            await _cacheService.RemoveData(cacheKey);

            await _cacheService.RemoveData("GetAllProducts_1000_Lasted");

            return new Response<string>(HttpStatusCode.OK, "Product deleted successfully!");
        }
    }
}