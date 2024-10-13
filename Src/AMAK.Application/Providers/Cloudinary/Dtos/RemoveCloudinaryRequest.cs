namespace AMAK.Application.Providers.Cloudinary.Dtos {
    public class RemoveCloudinaryRequest {
        public List<CloudinaryItem> Items { get; set; } = [];
    }

    public class CloudinaryItem {
        public string Id { get; set; } = null!;
    }
}