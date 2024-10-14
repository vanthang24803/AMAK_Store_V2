using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Prompt.Dtos {
    public class PromptRequest {
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public EPrompt Type { get; set; }
        public string Context { get; set; } = null!;
    }
}