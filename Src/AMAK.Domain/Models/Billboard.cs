namespace AMAK.Domain.Models {
    public class Billboard : BaseEntity<Guid> {
        public string? Thumbnail { get; set; }
        public string Url { get; set; } = null!;

        public string? PublicId { get; set; }
    }
}