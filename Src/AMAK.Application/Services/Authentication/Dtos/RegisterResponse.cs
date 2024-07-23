namespace AMAK.Application.Services.Authentication.Dtos {
    public record RegisterResponse(
        string Id,
        string FirstName,
        string LastName,
        string? Avatar,
        DateTime CreateAt) {
        public string FullName => $"{FirstName} {LastName}";
    }
}