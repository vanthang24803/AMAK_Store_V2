namespace AMAK.Domain.Models {
    public class Blog : BaseEntity<Guid> {
        public string Title { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public ApplicationUser Author { get; set; } = null!;
    }
}