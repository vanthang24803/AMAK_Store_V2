using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Product.Common {
    public class OptionProductUpdateRequest {
        public Guid? Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public int Sale { get; set; }
        public double Quantity { get; set; }
        public double IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}