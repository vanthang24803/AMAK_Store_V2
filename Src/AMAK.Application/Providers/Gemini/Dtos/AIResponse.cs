namespace AMAK.Application.Providers.Gemini.Dtos {
    public class AiResponse {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}