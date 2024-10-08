using System.ComponentModel.DataAnnotations;
using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class AccountConfig {
        [Key]
        public string Id { get; set; } = null!;
        public ELanguage Language { get; set; }
        public ETimezone Timezone { get; set; }
        public bool IsBan { get; set; }
        public bool IsActiveNotification { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}