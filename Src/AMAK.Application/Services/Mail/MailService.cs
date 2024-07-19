using AMAK.Application.Common.Exceptions;
using Microsoft.Extensions.Options;
using MimeKit;


namespace AMAK.Application.Services.Mail {
    public class MailService : IMailService {

        private readonly MailSettings mailSettings;

        private readonly Client.Domain _client;

        public MailService(IOptions<MailSettings> options, IOptions<Client.Domain> opt) {
            mailSettings = options.Value;
            _client = opt.Value;
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
    }
}