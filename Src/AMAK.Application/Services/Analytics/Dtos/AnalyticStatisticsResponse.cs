namespace AMAK.Application.Services.Analytics.Dtos {
    public class AnalyticStatisticsResponse {
        public Statistic Warehouse { get; set; } = null!;
        public Statistic Order { get; set; } = null!;
        public Statistic Revenue { get; set; } = null!;
        public Statistic SaleOut { get; set; } = null!;

    }


    public class Statistic {
        public bool IsStock { get; set; }

        public int Stock { get; set; }

        public double Total { get; set; }
    }
}