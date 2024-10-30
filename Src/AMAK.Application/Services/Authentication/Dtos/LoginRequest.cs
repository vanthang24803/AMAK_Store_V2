using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Authentication.Dtos {
    public class LoginRequest {
        [Required]
        [EmailAddress]
        [StringLength(128, ErrorMessage = "Email length can't be more than 128.")]
        public string Email { get; init; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,20}$",
            ErrorMessage = "Password must be 6-20 characters and contain at least one number, one special character, and one uppercase letter.")]
        public string Password { get; init; } = null!;
    }
}