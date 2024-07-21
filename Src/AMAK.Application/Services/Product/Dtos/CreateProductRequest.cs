using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Product.Dtos {
    public class CreateProductRequest {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;

        public string? Introduction { get; set; }
        public string? Specifications { get; set; }

        public List<Category> Categories { get; set; } = [];

        public List<Options> Options { get; set; } = [];
    }


    public class Category {
        public Guid Id { get; set; }
    }

    public class Options {
        public Guid Id { get; set; }
    }
}