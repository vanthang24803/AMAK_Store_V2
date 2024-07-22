
using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Authentication.Dtos {
    public class UpgradeRole {
        [Required]
        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}