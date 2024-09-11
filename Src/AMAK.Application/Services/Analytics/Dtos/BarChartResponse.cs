namespace AMAK.Application.Services.Analytics.Dtos {
    public class BarChartResponse {
        public Dictionary<string, double> Year { get; set; } = null!;
        public Dictionary<string, double> Month { get; set; } = null!;
        public Dictionary<string, double> Week { get; set; } = null!;

    }
}