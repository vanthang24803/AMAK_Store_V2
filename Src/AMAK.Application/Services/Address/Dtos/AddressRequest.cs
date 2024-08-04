using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Address.Dtos {
    public class AddressRequest {
        [Required]
        [StringLength(256)]
        public string AddressName { get; set; } = null!;

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(256)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string NumberPhone { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }
    }
}