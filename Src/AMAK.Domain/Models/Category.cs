
namespace AMAK.Domain.Models {
    public class Category : BaseEntity<Guid> {
        public required string Name { get; set; }

        public List<Product> Products { get; } = [];
    }
}