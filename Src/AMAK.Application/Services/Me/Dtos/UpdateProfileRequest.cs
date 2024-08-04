using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Me.Dtos {
    public class UpdateProfileRequest {
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(128)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string NumberPhone { get; set; } = null!;
    }
}