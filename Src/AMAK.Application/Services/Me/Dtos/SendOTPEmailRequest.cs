using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Me.Dtos {
    public class SendOTPEmailRequest {
        [Required]
        [StringLength(128)]
        public string Email { get; set; } = null!;
    }
}