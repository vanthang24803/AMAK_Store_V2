
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Services.Product.Commands.Delete {
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Product> _productRepository;

        public DeleteProductCommandHandler(IRepository<Domain.Models.Product> productRepository) {
            _productRepository = productRepository;
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
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

            return new Response<string>(HttpStatusCode.OK, "Product deleted successfully!");
        }
    }
}