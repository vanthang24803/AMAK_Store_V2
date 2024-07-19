using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Authentication.Dtos {
    public class RegisterRequest {
        [Required]
        [StringLength(128, ErrorMessage = "FirstName length can't be more than 128.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(128, ErrorMessage = "LastName length can't be more than 128.")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(128, ErrorMessage = "Email length can't be more than 128.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,20}$",
       ErrorMessage = "Password must be 6-20 characters and contain at least one number, one special character, and one uppercase letter.")]
        public string Password { get; set; } = null!;
    }
}