using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Category.Dtos {
    public class CategoryRequest {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
    }
}