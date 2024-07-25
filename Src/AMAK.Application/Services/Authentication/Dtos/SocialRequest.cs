
namespace AMAK.Application.Services.Authentication.Dtos {
    public record SocialRequest(
        string Email,
        string Name,
        string Avatar,
        string Provider
    );
}