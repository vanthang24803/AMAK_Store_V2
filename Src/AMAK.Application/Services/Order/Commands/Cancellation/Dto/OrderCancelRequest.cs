using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Order.Commands.Cancellation.Dto {
    public class OrderCancelRequest {
        public Guid OrderId { get; set; }

        [Required]
        public string Message { get; set; } = null!;
    }
}