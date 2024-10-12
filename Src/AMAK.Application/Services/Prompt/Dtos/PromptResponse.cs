using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Prompt.Dtos {
    public class PromptResponse {
        public Guid Id { get; set; }
        public EPrompt Type { get; set; }
        public string Context { get; set; } = null!;
        public DateTime UpdateAt { get; set; }

    }
}