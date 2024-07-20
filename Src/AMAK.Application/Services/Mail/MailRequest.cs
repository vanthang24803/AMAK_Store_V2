namespace AMAK.Application.Services.Mail {
    public class MailRequest {
        public string ToEmail { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Message { get; set; } = null!;

    }
}