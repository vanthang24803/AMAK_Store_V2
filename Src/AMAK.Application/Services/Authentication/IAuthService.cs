using AMAK.Application.Dtos.Auth;

namespace AMAK.Application.Services.Authentication {
    public interface IAuthService {
        Task<string> RegisterAsync(RegisterRequest request);
    }
}