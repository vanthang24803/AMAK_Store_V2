using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Categories.Common {
    public class CategoryRequest {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
    }
}