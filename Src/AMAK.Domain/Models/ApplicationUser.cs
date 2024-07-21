using Microsoft.AspNetCore.Identity;

namespace AMAK.Domain.Models {
    public class ApplicationUser : IdentityUser {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Avatar { get; set; } = null!;

        public ICollection<Address> Addresses { get; set; } = [];

        public ICollection<Review> Reviews { get; set; } = [];

        public ICollection<Order> Orders { get; set; } = [];

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}