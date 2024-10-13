
namespace AMAK.Application.Providers.Cloudinary.Dtos {
    public class CloudinaryResponse {
        public string PublicId { get; set; } = null!;

        public string Format { get; set; } = null!;

        public string ResourceType { get; set; } = null!;

        public Uri Url { get; set; } = null!;

        public Uri SecureUrl { get; set; } = null!;

        public long Bytes { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string CreatedAt { get; set; } = null!;
    }
}