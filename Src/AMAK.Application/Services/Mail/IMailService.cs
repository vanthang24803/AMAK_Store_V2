using AMAK.Domain.Models;

namespace AMAK.Application.Services.Mail {
    public interface IMailService {

        void SendEmailConfirmationAccount(string email , string userId , string token);
        Task SendMailAsync(MailRequest mailRequest);
    }
}