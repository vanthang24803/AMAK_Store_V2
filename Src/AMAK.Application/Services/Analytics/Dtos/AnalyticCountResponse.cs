namespace AMAK.Application.Services.Analytics.Dtos {
    public class AnalyticCountResponse {
        public long Archive { get; set; }
        public long Active { get; set; }
        public long Orders { get; set; }
        public long Categories { get; set; }
        public long Customers { get; set; }
    }
}