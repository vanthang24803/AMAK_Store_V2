
namespace AMAK.Application.Services.Analytics.Dtos {
    public class AreaChartResponse {
        public List<DataEntry> Day { get; set; } = [];
        public List<DataEntry> Month { get; set; } = [];
        public List<DataEntry> Week { get; set; } = [];
    }

    public class DataEntry {
        public string Date { get; set; } = null!;
        public long Input { get; set; }
        public long Output { get; set; }
    }
}