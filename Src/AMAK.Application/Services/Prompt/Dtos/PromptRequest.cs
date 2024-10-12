using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Prompt.Dtos {
    public class PromptRequest {
        public EPrompt Type { get; set; }
        public string Context { get; set; } = null!;
    }
}