namespace AMAK.Application.Providers.Configuration.Dtos {
    public class MailSettings {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Host { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public int Port { get; set; }
    }
}