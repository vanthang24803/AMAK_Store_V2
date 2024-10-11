
using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class Prompt : BaseEntity<Guid> {
        public EPrompt Type { get; set; }
        public string Context { get; set; } = null!;
    }
}