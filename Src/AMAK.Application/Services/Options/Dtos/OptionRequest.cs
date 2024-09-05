using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Options.Dtos {
    public class OptionRequest {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        public int Sale { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}