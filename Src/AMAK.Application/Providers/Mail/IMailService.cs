using AMAK.Application.Providers.Mail.Dtos;

namespace AMAK.Application.Providers.Mail {
    public interface IMailService {
        Task SendEmailConfirmationAccount(MailWithTokenEvent mail);
        Task SendMailResetPassword(MailWithTokenEvent mail);
        Task SendOrderMail(OrderMailEvent request);
        Task SendMailAsync(MailRequest mailRequest);
    }
}