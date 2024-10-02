
using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Tickets.Dtos {
    public class FindTicketRequest {
        [Required]
        public string Code { get; set; } = null!;
    }
}