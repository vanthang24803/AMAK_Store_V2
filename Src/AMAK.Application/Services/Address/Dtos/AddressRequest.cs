using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Address.Dtos {
    public class AddressRequest {
        [Required]
        [StringLength(256)]
        public string Name { get; set; } = null!;
        [Required]
        public bool IsActive { get; set; }
    }
}