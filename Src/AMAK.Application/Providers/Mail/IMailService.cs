namespace AMAK.Application.Providers.Mail {
    public interface IMailService {
        void SendEmailConfirmationAccount(string email, string userId, string token);
        void SendMailResetPassword(string email, string userId, string token);
        Task SendMailAsync(MailRequest mailRequest);
    }
}