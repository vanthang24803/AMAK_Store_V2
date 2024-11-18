using Microsoft.AspNetCore.Http;

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

    }
}