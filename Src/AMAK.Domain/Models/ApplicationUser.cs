using Microsoft.AspNetCore.Identity;

namespace AMAK.Domain.Models {
    public class ApplicationUser : IdentityUser {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Avatar { get; set; }
        public int Point { get; set; } = 0;
        public ICollection<Address> Addresses { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Notification> Notifications { get; } = [];
        public Cart? Cart { get; set; }
        public AccountConfig? Config { get; set; }
        public ICollection<Blog> Blogs { get; set; } = [];
        public ICollection<Conversation> Collections { get; set; } = [];
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}