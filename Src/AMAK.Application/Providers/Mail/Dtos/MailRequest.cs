namespace AMAK.Application.Providers.Mail.Dtos {
    public class MailRequest {
        public string ToEmail { get; init; } = null!;
        public string Subject { get; init; } = null!;
        public string Message { get; init; } = null!;

    }
}