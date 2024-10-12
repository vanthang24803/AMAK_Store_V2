namespace AMAK.Application.Providers.Configuration.Dtos {
    public class MomoSettings {
        public string PartnerCode { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
        public string IpnUrl { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string PaymentUrl { get; set; } = null!;
    }
}