
namespace AMAK.Application.Services.Trash.Dtos {
    public class OptionDeletedResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Sale { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}