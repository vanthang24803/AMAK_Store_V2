using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtpNet;

namespace AMAK.Application.Common.Helpers {
    public class OTPService {
        private readonly string _secretKey = "Hello World";

        private readonly ILogger _logger;

        public OTPService(IConfiguration configuration, ILogger<OTPService> logger) {
            // _secretKey = configuration["JWT:Secret"] ?? "";
            _logger = logger;
        }

        public string GenerateOTP(int digits = 6, int step = 30) {
            _logger.LogInformation("Secret {}", _secretKey);
            byte[] secretKeyBytes = Base32Encoding.ToBytes(_secretKey);

            var totp = new Totp(secretKeyBytes, step: step, totpSize: digits);

            string otp = totp.ComputeTotp();

            return otp;
        }


        public bool VerifyOTP(string otp, int digits = 6, int step = 30) {
            byte[] secretKeyBytes = Base32Encoding.ToBytes(_secretKey);

            var totp = new Totp(secretKeyBytes, step: step, totpSize: digits);
            return totp.VerifyTotp(otp, out _, VerificationWindow.RfcSpecifiedNetworkDelay);
        }

    }
}