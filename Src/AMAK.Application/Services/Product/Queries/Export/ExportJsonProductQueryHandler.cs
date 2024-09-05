using System.Text.Json;
using System.Text.Json.Serialization;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Product.Queries.Export;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class ExportJsonProductQueryHandler : IRequestHandler<ExportJsonProductQuery, byte[]> {

    private readonly IRepository<AMAK.Domain.Models.Product> _productRepository;

    public ExportJsonProductQueryHandler(IRepository<AMAK.Domain.Models.Product> productRepository) {
        _productRepository = productRepository;
    }

    public async Task<byte[]> Handle(ExportJsonProductQuery request, CancellationToken cancellationToken) {
        var products = await _productRepository.GetAll()
            .Include(c => c.Categories)
            .Include(o => o.Options)
            .Include(r => r.Reviews)
            .Include(p => p.Photos)
            .ToListAsync(cancellationToken);

        var options = new JsonSerializerOptions {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        var jsonString = JsonSerializer.Serialize(products, options);

        return System.Text.Encoding.UTF8.GetBytes(jsonString);
    }
}
