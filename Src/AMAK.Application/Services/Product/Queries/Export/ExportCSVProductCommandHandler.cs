using AMAK.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AMAK.Application.Services.Product.Queries.Export {
    public class ExportCSVProductCommandHandler : IRequestHandler<ExportCSVProductCommand, byte[]> {

        private readonly IRepository<Domain.Models.Product> _productRepository;

        public ExportCSVProductCommandHandler(IRepository<Domain.Models.Product> productRepository) {
            _productRepository = productRepository;
        }

        public async Task<byte[]> Handle(ExportCSVProductCommand request, CancellationToken cancellationToken) {
            // Fetch all products with related entities
            var products = await _productRepository.GetAll()
                .Include(c => c.Categories)
                .Include(p => p.Photos)
                .Include(o => o.Options)
                .ToListAsync(cancellationToken: cancellationToken);

            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine("Id,Name,Brand,Thumbnail,Sold,Categories,Options,Prices,Sales,Quantities,Media");

            foreach (var product in products) {
                var categoryNames = string.Join(" | ", product.Categories.Select(c => c.Name));
                var optionNames = string.Join(" | ", product.Options.Select(o => o.Name));
                var optionPrices = string.Join(" | ", product.Options.Select(o => o.Price));
                var optionSales = string.Join(" | ", product.Options.Select(o => o.Sale));
                var optionQuantities = string.Join(" | ", product.Options.Select(o => o.Quantity));
                var photoUrls = string.Join(" | ", product.Photos.Select(p => p.Url));

                csvBuilder.AppendLine($"{product.Id},{product.Name},{product.Brand},{product.Thumbnail},{product.Sold},{categoryNames},{optionNames},{optionPrices},{optionSales},{optionQuantities},{photoUrls}");
            }

            var preamble = Encoding.UTF8.GetPreamble();
            var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            return [.. preamble, .. csvBytes];
        }
    }
}
