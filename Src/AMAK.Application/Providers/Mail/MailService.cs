using System.Text;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Configuration;
using AMAK.Application.Providers.Mail.Dtos;
using AMAK.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MimeKit;


namespace AMAK.Application.Providers.Mail {
    public class MailService : IMailService {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IRepository<EmailTemplate> _emailTemplatesRepository;
        private readonly string WebClient;

        public MailService(IRepository<EmailTemplate> emailTemplatesRepository, IConfigurationProvider configurationProvider, Microsoft.Extensions.Configuration.IConfiguration configuration) {
            WebClient = configuration["Client:Web"]!;
            _emailTemplatesRepository = emailTemplatesRepository;
            _configurationProvider = configurationProvider;
        }

        public async Task SendEmailConfirmationAccount(MailWithTokenEvent mail) {
            var existingTemplate = await _emailTemplatesRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Name == Domain.Enums.ETemplate.VERIFY_ACCOUNT)
                ?? throw new NotFoundException("Verify Email Template Not Found!");

            string htmlContent = existingTemplate.Template;

            string verifyLink = $"{WebClient}/verify-account?userId={mail.UserId}&token=${mail.Token}";

            htmlContent = htmlContent.Replace("{USERNAME}", mail.FullName);
            htmlContent = htmlContent.Replace("{VERIFICATION_LINK}", verifyLink);

            try {
                MailRequest mailRequest = new() {
                    ToEmail = mail.Email,
                    Subject = "Xác nhận tài khoản",
                    Message = htmlContent,

                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task SendMailResetPassword(MailWithTokenEvent mail) {

            var existingTemplate = await _emailTemplatesRepository.GetAll()
              .FirstOrDefaultAsync(x => x.Name == Domain.Enums.ETemplate.FORGOT_PASSWORD)
              ?? throw new NotFoundException("Forgot Password Template Not Found!");

            string htmlContent = existingTemplate.Template;

            string verifyLink = $"{WebClient}/reset-password?userId={mail.UserId}&token=${mail.Token}";

            htmlContent = htmlContent.Replace("{USERNAME}", mail.FullName);
            htmlContent = htmlContent.Replace("{LINK}", verifyLink);

            try {
                MailRequest mailRequest = new() {
                    ToEmail = mail.Email,
                    Subject = "Đổi mật khẩu",
                    Message = htmlContent

                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task SendMailAsync(MailRequest mailRequest) {
            var mailSettings = (await _configurationProvider.GetEmailSettingAsync()).Result;

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

        public async Task SendOrderMail(OrderMailEvent request) {
            try {
                MailRequest mailRequest = new() {
                    ToEmail = request.To,
                    Subject = request.Subject,
                    Message = await OrderMail(request.Order, request.OrderResponses)
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