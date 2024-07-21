using AMAK.Application.Services.Category.Dtos;

namespace AMAK.Application.Services.Product.Dtos {
    public class ProductResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<CategoryResponse> Categories { get; set; } = [];
        public List<Options> Options { get; set; } = [];
        public DateTime CreateAt { get; set; }
    }
}