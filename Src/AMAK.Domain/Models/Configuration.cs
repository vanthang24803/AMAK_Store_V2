namespace AMAK.Domain.Models {
    public class Configuration : BaseEntity<Guid> {
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}