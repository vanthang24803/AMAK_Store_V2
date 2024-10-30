namespace AMAK.Application.Services.Chat.Dtos {
    public abstract record MessageRequest(
        string Content,
        string FromUserId,
        string ToUserId
    );
}