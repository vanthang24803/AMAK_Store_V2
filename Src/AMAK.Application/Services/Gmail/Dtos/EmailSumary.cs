namespace AMAK.Application.Services.Gmail.Dtos {
    public class EmailSummary {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
    }

}