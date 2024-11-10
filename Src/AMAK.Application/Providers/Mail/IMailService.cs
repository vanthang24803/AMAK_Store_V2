using AMAK.Application.Providers.Mail.Dtos;
using AMAK.Domain.Models;

namespace AMAK.Application.Providers.Mail {
    public interface IMailService {
        Task SendEmailConfirmationAccount(string email, string fullName, string userId, string token);
        Task SendMailResetPassword(string email, string fullName, string userId, string token);
        Task SendOrderMail(string to, string subject, Order order, List<OrderDetail> orderResponses);
        Task SendMailAsync(MailRequest mailRequest);
    }
}