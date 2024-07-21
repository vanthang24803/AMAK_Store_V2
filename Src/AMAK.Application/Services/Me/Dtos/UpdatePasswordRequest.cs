using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Me.Dtos {
    public class UpdatePasswordRequest {
        [Required(ErrorMessage = "Old password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,20}$",
       ErrorMessage = "Password must be 6-20 characters and contain at least one number, one special character, and one uppercase letter.")]

        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password is required")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,20}$",
       ErrorMessage = "Password must be 6-20 characters and contain at least one number, one special character, and one uppercase letter.")]


        public string NewPassword { get; set; } = null!;
    }
}