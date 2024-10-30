namespace AMAK.Application.Services.Chat.Dtos {
    public record MessageRequest(
        string Content,
        string FromUserId,
        string ToUserId
    );
}