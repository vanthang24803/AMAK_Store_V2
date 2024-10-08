using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class EmailTemplate : BaseEntity<Guid> {
        public ETemplate Name { get; set; }
        public string Template { get; set; } = null!;
    }
}