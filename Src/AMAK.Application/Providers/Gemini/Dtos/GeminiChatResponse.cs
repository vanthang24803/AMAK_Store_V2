namespace AMAK.Application.Providers.Gemini.Dtos;

public class GeminiChatResponse {
    public Guid Id { get; set; }
    public string Message { get; set; } = null!;
    public bool IsBotReply { get; set; }
    public DateTime CreateAt { get; set; }
}