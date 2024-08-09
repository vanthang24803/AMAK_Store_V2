namespace AMAK.Application.Services.Order.Dtos {
    public class UpdateOrderByAccountRequest {
        public string Customer { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string NumberPhone { get; set; } = null!;
    }
}