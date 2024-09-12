namespace AMAK.Application.Services.Analytics.Dtos {
    public class AnalyticTopCustomerResponse {
        public List<TopCustomer> Day { get; set; } = [];
        public List<TopCustomer> Month { get; set; } = [];
        public List<TopCustomer> Week { get; set; } = [];
    }

    public class TopCustomer {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string Rank { get; set; } = null!;
        public double TotalPrice { get; set; }
    }
}