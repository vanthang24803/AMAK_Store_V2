using AMAK.Application.Services.FlashSale;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AMAK.Application.Providers.Tasks {
    public class FlashSaleJob : IJob {
        private readonly ILogger<FlashSaleJob> _logger;
        private readonly IFlashSaleService _flashSaleService;

        public FlashSaleJob(IFlashSaleService flashSaleService, ILogger<FlashSaleJob> logger) {
            _flashSaleService = flashSaleService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context) {
            await _flashSaleService.JobFlashSale();
            _logger.LogInformation("Handle Sale at {UtcNow} successfully!", DateTime.UtcNow);
        }
    }
}