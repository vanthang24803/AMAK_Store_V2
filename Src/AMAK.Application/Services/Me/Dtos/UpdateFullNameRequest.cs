using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Me.Dtos {
    public class UpdateFullNameRequest {
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(128)]
        public string LastName { get; set; } = null!;
    }
}