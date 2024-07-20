
using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Authentication.Dtos {
    public class ResetPasswordRequest {
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,20}$",
            ErrorMessage = "Password must be 6-20 characters and contain at least one number, one special character, and one uppercase letter.")]
        public string NewPassword { get; set; } = null!;
    }
}