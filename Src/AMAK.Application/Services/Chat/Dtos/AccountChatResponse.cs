namespace AMAK.Application.Services.Chat.Dtos {
    public class AccountChatResponse {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string? LastMessage { get; set; }
    }
}