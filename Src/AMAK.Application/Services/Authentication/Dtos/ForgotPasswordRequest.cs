

using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Authentication.Dtos {
    public class ForgotPasswordRequest {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}