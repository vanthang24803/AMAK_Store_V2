
namespace AMAK.Application.Services.Authentication.Dtos {
    public class TokenResponse {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}