using System.Text;
using AMAK.Application.Common.Exceptions;
using AMAK.Domain.Models;
using Microsoft.Extensions.Options;
using MimeKit;


namespace AMAK.Application.Providers.Mail {
    public class MailService : IMailService {

        private readonly MailSettings mailSettings;

        public MailService(IOptions<MailSettings> options) {
            mailSettings = options.Value;
        }

        public async void SendEmailConfirmationAccount(string email, string userId, string token) {
            try {
                MailRequest mailRequest = new() {
                    ToEmail = email,
                    Subject = "Verify account",
                    Message = "<a href='" + "http://localhost:3000/" + "/verify-account?userId=" + userId + "&token=" + token
                   + "' target='_blank'>Click here to verify your account</a>"

                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        public async void SendMailResetPassword(string email, string userId, string token) {
            try {
                MailRequest mailRequest = new() {
                    ToEmail = email,
                    Subject = "Verify account",
                    Message = "<a href='" + "http://localhost:3000/" + "/reset-password?userId=" + userId + "&token=" + token
                   + "' target='_blank'>Click here to reset password</a>"

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

        public async Task SendOrderMail(string to, string subject, Order order, List<Domain.Models.OrderDetail> orderResponses) {
            try {
                MailRequest mailRequest = new() {
                    ToEmail = to,
                    Subject = subject,
                    Message = OrderMail(order, orderResponses)
                };

                await SendMailAsync(mailRequest);

            } catch (Exception ex) {
                throw new BadRequestException(ex.Message);
            }
        }

        private static string OrderMail(Order order, List<Domain.Models.OrderDetail> orderResponses) {
            string htmlContent = "";

            try {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Order.html");
                htmlContent = File.ReadAllText(path);

            } catch (Exception e) {
                Console.WriteLine(e);
            }


            htmlContent = htmlContent.Replace("{ID}", order.Id.ToString());
            htmlContent = htmlContent.Replace("{CUSTOMER}", order.Customer);
            htmlContent = htmlContent.Replace("{ADDRESS}", order.Address);
            htmlContent = htmlContent.Replace("{PHONE}", order.NumberPhone);
            htmlContent = htmlContent.Replace("{QUANTITY}", order.Quantity.ToString());
            htmlContent = htmlContent.Replace("{TOTAL_PRICE}", order.TotalPrice.ToString());
            htmlContent = htmlContent.Replace("{PAYMENT}", order.Payment.ToString());
            htmlContent = htmlContent.Replace("{STATUS}", order.Status.ToString());

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