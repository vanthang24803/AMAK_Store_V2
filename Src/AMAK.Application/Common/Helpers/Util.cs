using Microsoft.AspNetCore.Http;
using OtpNet;

namespace AMAK.Application.Common.Helpers {
    public static class Util {
        public static string ConvertImageToBase64(IFormFile file) {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }

        public static IFormFile ConvertBase64ToImage(string base64) {

            byte[] fileBytes = Convert.FromBase64String(base64);
            var stream = new MemoryStream(fileBytes);

            string fileName = $"{Guid.NewGuid()}.webp";

            return new FormFile(stream, 0, fileBytes.Length, "file", fileName);
        }

        public static string GenerateOTP(int digits = 6, int step = 30) {
            byte[] secretKeyBytes = KeyGeneration.GenerateRandomKey(20);
            var totp = new Totp(secretKeyBytes, step: step, totpSize: digits);

            string otp = totp.ComputeTotp();

            return otp;
        }
    }
}