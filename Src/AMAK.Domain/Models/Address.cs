using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class Address : BaseEntity<Guid> {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}