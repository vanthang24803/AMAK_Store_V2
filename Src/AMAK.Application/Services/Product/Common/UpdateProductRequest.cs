
using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Product.Common {
    public class UpdateProductRequest {
        [Required]
        public string Name { get; set; } = null!;

        public string? Introduction { get; set; }
        public string? Specifications { get; set; }
    }
}