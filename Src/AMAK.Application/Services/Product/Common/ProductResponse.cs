using AMAK.Application.Services.Category.Dtos;
using AMAK.Application.Services.Options.Dtos;

namespace AMAK.Application.Services.Product.Common {
    public class ProductResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<CategoryResponse> Categories { get; set; } = [];
        public List<OptionResponse> Options { get; set; } = [];
        public DateTime CreateAt { get; set; }
    }
}