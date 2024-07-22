using System.ComponentModel.DataAnnotations;
using AMAK.Application.Services.Options.Dtos;

namespace AMAK.Application.Services.Product.Common {
    public class UpdateProductRequest {
        [Required]
        public string Name { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string? Introduction { get; set; }
        public string? Specifications { get; set; }

        public List<UpdateCategory> Categories { get; set; } = [];

        public List<OptionProductUpdate> Options { get; set; } = [];


    }

    public class UpdateCategory {
        public Guid Id { get; set; }
    }


    public class OptionProductUpdate {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
        public int Sale { get; set; }
        public double Quantity { get; set; }
        public double IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }


}