using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Me.Dtos {
    public class UpdateEmailRequest {
        [Required]
        [StringLength(128)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(6)]
        public string Code { get; set; } = null!;
    }
}