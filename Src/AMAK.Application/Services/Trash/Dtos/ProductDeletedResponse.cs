
namespace AMAK.Application.Services.Trash.Dtos {
    public class ProductDeletedResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public int Options { get; set; }
        public int Photos { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}