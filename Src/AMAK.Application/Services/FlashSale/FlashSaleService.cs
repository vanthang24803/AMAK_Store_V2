using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Cache;
using Microsoft.Extensions.Logging;

namespace AMAK.Application.Services.FlashSale {
    public class FlashSaleService : IFlashSaleService {
        private readonly ILogger<FlashSaleService> _logger;
        private readonly ICacheService _cacheService;
        private readonly IRepository<Domain.Models.FlashSale> _flashSaleRepository;
        private readonly IRepository<Domain.Models.Product> _productRepository;
        private readonly IRepository<Domain.Models.FlashSaleProduct> _flashSaleProductRepository;

        public FlashSaleService(IRepository<Domain.Models.FlashSale> flashSaleRepository, ICacheService cacheService, ILogger<FlashSaleService> logger, IRepository<Domain.Models.Product> productRepository, IRepository<Domain.Models.FlashSaleProduct> flashSaleProductRepository) {
            _flashSaleRepository = flashSaleRepository;
            _cacheService = cacheService;
            _logger = logger;
            _productRepository = productRepository;
            _flashSaleProductRepository = flashSaleProductRepository;
        }
    }
}