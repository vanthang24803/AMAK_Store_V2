using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Template.Dtos {
    public class TemplateRequest {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ETemplate Type { get; set; }
        public string Template { get; set; } = null!;

    }
}