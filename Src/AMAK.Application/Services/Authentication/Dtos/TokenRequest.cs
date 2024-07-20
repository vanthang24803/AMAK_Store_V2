using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Authentication.Dtos {
    public class TokenRequest {
        [Required]
        public string Token { get; set; } = null!;
    }
}