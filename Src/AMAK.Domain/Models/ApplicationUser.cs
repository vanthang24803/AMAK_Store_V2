using Microsoft.AspNetCore.Identity;

namespace AMAK.Domain.Models {
    public class ApplicationUser : IdentityUser {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Avatar { get; set; } = string.Empty;

        public IEnumerable<Address> Addresses { get; set; } = [];

        public IEnumerable<Review> Reviews { get; set; } = [];

        public IEnumerable<Order> Orders { get; set; } = [];

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}