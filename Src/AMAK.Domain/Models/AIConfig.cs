using System.Text.Json;

namespace AMAK.Domain.Models {
    public class AIConfig : BaseEntity<Guid> {
        public string Name { get; set; } = null!;
        public JsonDocument? Config { get; set; }
    }
}