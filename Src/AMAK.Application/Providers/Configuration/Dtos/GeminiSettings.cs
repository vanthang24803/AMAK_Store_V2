namespace AMAK.Application.Providers.Configuration.Dtos {
    public class GeminiSettings {
        public string ApiKey { get; set; } = null!;
        public string ProjectNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
    }
}