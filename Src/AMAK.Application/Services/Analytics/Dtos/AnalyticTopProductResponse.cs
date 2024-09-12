namespace AMAK.Application.Services.Analytics.Dtos {
    public class AnalyticTopProductResponse {
        public List<TopProduct> Day { get; set; } = [];
        public List<TopProduct> Month { get; set; } = [];
        public List<TopProduct> Week { get; set; } = [];
    }

    public class TopProduct {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public long Sold { get; set; }
    }
}