using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Prompt.Dtos {
    public class PromptResponse {
        public Guid Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPrompt Type { get; set; }
        public string Context { get; set; } = null!;
        public DateTime UpdateAt { get; set; }

    }
}