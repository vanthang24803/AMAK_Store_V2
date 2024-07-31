using AMAK.Application.Services.Order.Dtos;
using AMAK.Domain.Models;

namespace AMAK.Application.Providers.Mail {
    public interface IMailService {
        void SendEmailConfirmationAccount(string email, string userId, string token);
        void SendMailResetPassword(string email, string userId, string token);

        Task SendOrderMail(string to, string subject, Order order, List<Domain.Models.OrderDetail> orderResponses);
        Task SendMailAsync(MailRequest mailRequest);
    }
}