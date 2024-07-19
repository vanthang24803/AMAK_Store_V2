namespace AMAK.Application.Services.Authentication.Dtos {
    public class RegisterResponse {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public string? Avatar { get; set; }
        public DateTime CreateAt { get; set; }
    }
}