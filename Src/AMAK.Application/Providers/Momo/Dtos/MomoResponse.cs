namespace AMAK.Application.Providers.Momo {
    public class MomoResponse {
        public string PartnerCode { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public string RequestId { get; set; } = null!;
        public int Amount { get; set; }
        public long ResponseTime { get; set; }
        public string Message { get; set; } = null!;
        public int ResultCode { get; set; }
        public string PayUrl { get; set; } = null!;
        public string Deeplink { get; set; } = null!;
        public string QrCodeUrl { get; set; } = null!;
    }
}