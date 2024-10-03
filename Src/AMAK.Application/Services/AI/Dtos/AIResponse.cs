namespace AMAK.Application.Services.AI.Dtos {
    public class AIResponse {
        public string Message { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Timestamp { get; set; } = null!;
    }
}