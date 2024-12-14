
using AMAK.Application.Services.Tickets;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AMAK.Application.Providers.Tasks {
    public class TicketJob : IJob {
        private readonly ILogger<TicketJob> _logger;
        private readonly ITicketService _ticketService;

        public TicketJob(ILogger<TicketJob> logger, ITicketService ticketService) {
            _logger = logger;
            _ticketService = ticketService;
        }

        public async Task Execute(IJobExecutionContext context) {

            await _ticketService.CheckTicketJob();
            _logger.LogInformation("Check ticket at {UtcNow} successfully!", DateTime.UtcNow);
        }
    }
}