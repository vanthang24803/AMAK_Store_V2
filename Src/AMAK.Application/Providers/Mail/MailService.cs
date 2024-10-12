using System.Text;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Configuration.Dtos;
using AMAK.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;


namespace AMAK.Application.Providers.Mail {
    public class MailService : IMailService {

        private readonly MailSettings mailSettings;

        private readonly IRepository<EmailTemplate> _emailTemplatesRepository;

        public MailService(IOptions<MailSettings> options, IRepository<EmailTemplate> emailTemplatesRepository) {
            _emailTemplatesRepository = emailTemplatesRepository;
            mailSettings = options.Value;
        }

        public async void SendEmailConfirmationAccount(string email, string fullName, string userId, string token) {
            var existingTemplate = await _emailTemplatesRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == Domain.Enums.ETemplate.VERIFY_ACCOUNT)
                ?? throw new NotFoundException("Verify Email Template Not Found!");

            string htmlContent = existingTemplate.Template;

            string verifyLink = $"http://localhost:3000/verify-account?userId={userId}&token=${token}";

            htmlContent = htmlContent.Replace("{USERNAME}", fullName);
            htmlContent = htmlContent.Replace("{VERIFICATION_LINK}", verifyLink);

            try {
                MailRequest mailRequest = new() {
                    ToEmail = email,
                    Subject = "Xác nhận tài khoản",
                    Message = htmlContent,

                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        public async void SendMailResetPassword(string email, string fullName, string userId, string token) {

            var existingTemplate = await _emailTemplatesRepository.GetAll()
              .FirstOrDefaultAsync(x => x.Name == Domain.Enums.ETemplate.FORGOT_PASSWORD)
              ?? throw new NotFoundException("Forgot Password Template Not Found!");

            string htmlContent = existingTemplate.Template;

            string verifyLink = $"http://localhost:3000/verify-account?userId={userId}&token=${token}";

            htmlContent = htmlContent.Replace("{USERNAME}", fullName);
            htmlContent = htmlContent.Replace("{LINK}", verifyLink);

            try {
                MailRequest mailRequest = new() {
                    ToEmail = email,
                    Subject = "Đổi mật khẩu",
                    Message = htmlContent

                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task SendMailAsync(MailRequest mailRequest) {
            var email = new MimeMessage {
                Sender = MailboxAddress.Parse(mailSettings.Email)
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder {
                HtmlBody = mailRequest.Message
            };
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(mailSettings.Host, mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailSettings.Email, mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendOrderMail(string to, string subject, Order order, List<OrderDetail> orderResponses) {
            try {
                MailRequest mailRequest = new() {
                    ToEmail = to,
                    Subject = subject,
                    Message = await OrderMail(order, orderResponses)
                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        private async Task<string> OrderMail(Order order, List<OrderDetail> orderResponses) {
            var existingTemplate = await _emailTemplatesRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Name == Domain.Enums.ETemplate.ORDER) ?? throw new NotFoundException("Order Template Not Found");

            string htmlContent = existingTemplate.Template;


            var latestStatus = order.Status
              .OrderBy(s => s.TimeStamp)
              .LastOrDefault();


            htmlContent = htmlContent.Replace("{ID}", order.Id.ToString());
            htmlContent = htmlContent.Replace("{CUSTOMER}", order.Customer);
            htmlContent = htmlContent.Replace("{ADDRESS}", order.Address);
            htmlContent = htmlContent.Replace("{PHONE}", order.NumberPhone);
            htmlContent = htmlContent.Replace("{QUANTITY}", order.Quantity.ToString());
            htmlContent = htmlContent.Replace("{TOTAL_PRICE}", order.TotalPrice.ToString());
            htmlContent = htmlContent.Replace("{PAYMENT}", order.Payment.ToString());
            htmlContent = htmlContent.Replace("{STATUS}", latestStatus?.Status.ToString());

            StringBuilder detailsBuilder = new();
            foreach (var detail in orderResponses) {
                detailsBuilder.Append("<tr>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.ProductName).Append("</td>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.Price).Append(" VNĐ</td>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.OptionName).Append("</td>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.Quantity).Append("</td>")
                    .Append("</tr>");
            }

            htmlContent = htmlContent.Replace("{DETAILS}", detailsBuilder.ToString());


            return htmlContent;
        }
    }
}