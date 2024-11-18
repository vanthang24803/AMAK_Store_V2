namespace AMAK.Application.Providers.RabbitMq.Common {
    public class RabbitUpload {
        public Guid ProductId { get; set; }
        public List<string> Files { get; set; } = [];
    }
}