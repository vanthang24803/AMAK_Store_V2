namespace AMAK.Domain.Models {
    public class Product : BaseEntity<Guid> {
        public string Name { get; set; } = null!;
        public string? Thumbnail { get; set; }
        public string? Brand { get; set; }
        public long Sold { get; set; }
        public string? Introduction { get; set; }
        public string? Specifications { get; set; }
        public ICollection<Photo> Photos { get; set; } = [];
        public ICollection<Option> Options { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public List<Category> Categories { get; } = [];

    }
}