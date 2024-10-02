using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Tickets.Dtos {
    public class TicketSchema {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = null!;
        [MaxLength(10)]
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }

}