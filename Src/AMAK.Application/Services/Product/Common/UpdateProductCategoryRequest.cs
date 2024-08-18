using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Product.Common {
    public class UpdateProductCategoryRequest {
        public List<CategoryProductRequest> Categories { get; set; } = [];
    }

    public class CategoryProductRequest {
        [Required]
        public Guid Id { get; set; }
    }
}