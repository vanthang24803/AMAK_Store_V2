using AMAK.Application.Services.Category.Dtos;

namespace AMAK.Application.Services.Product.Dtos {
    public class ProductDetailResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public long Sold { get; set; }
        public string? Introduction { get; set; }
        public string? Specifications { get; set; }

        public List<CategoryResponse> Categories { get; set; } = [];
        public List<Options> Options { get; set; } = [];
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}