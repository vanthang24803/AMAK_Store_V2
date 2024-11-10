using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class Address : BaseEntity<Guid> {
        public string AddressName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NumberPhone { get; set; }
        public bool IsActive { get; set; }
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}