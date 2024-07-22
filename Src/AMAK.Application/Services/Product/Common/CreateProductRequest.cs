using System.ComponentModel.DataAnnotations;
using AMAK.Application.Services.Options.Dtos;

namespace AMAK.Application.Services.Product.Common {
    public class CreateProductRequest {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(128)]

        public string Brand { get; set; } = null!;

        public string? Introduction { get; set; }
        public string? Specifications { get; set; }

        public List<Category> Categories { get; set; } = [];

        public List<OptionRequest> Options { get; set; } = [];
    }


    public class Category {
        public Guid Id { get; set; }
    }


}