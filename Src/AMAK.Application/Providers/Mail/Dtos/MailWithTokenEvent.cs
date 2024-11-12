using AMAK.Application.Constants;

namespace AMAK.Application.Providers.Mail.Dtos {
    public class MailWithTokenEvent {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public EEmailType Type { get; set; }

    }
}