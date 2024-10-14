
namespace AMAK.Application.Providers.Configuration.Dtos {
    public class Config {
        public CloudinarySettings Cloudinary { get; set; } = null!;
        public MailSettings Mail { get; set; } = null!;
        public GoogleSettings Google { get; set; } = null!;
        public MomoSettings Momo { get; set; } = null!;
        public GeminiSettings GeminiSettings { get; set; } = null!;
    }
}