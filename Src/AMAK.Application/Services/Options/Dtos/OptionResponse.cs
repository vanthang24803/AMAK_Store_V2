namespace AMAK.Application.Services.Options.Dtos {
    public class OptionResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Sale { get; set; }
        public double Quantity { get; set; }
        public double IsActive { get; set; }
        public DateTime CreateAt { get; set; }
    }
}