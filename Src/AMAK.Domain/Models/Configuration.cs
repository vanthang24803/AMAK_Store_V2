using System.Text.Json;

namespace AMAK.Domain.Models {
    public class Configuration : BaseEntity<Guid> {
        public string Key { get; set; } = null!;
        public JsonElement? Value { get; set; }
    }
}