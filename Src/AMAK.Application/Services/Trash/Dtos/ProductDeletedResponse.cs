
namespace AMAK.Application.Services.Trash.Dtos {
    public class ProductDeletedResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public long Sold { get; set; }
        public int Options { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}