namespace AMAK.Application.Services.Trash.Dtos {
    public class PhotoDeletedResponse {
        public Guid Id { get; set; }

        public string Url { get; set; } = null!;

        public DateTime DeletedAt { get; set; }
    }
}