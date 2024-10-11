using AMAK.Domain.Models;

namespace AMAK.Application.Providers.Mail {
    public interface IMailService {
        void SendEmailConfirmationAccount(string email, string fullName, string userId, string token);
        void SendMailResetPassword(string email, string fullName, string userId, string token);

        Task SendOrderMail(string to, string subject, Order order, List<OrderDetail> orderResponses);
        Task SendMailAsync(MailRequest mailRequest);
    }
}