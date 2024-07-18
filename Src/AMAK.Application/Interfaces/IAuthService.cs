
using AMAK.Application.Dtos.Auth;

namespace AMAK.Application.Interfaces {
    public interface IAuthService {
        Task<string> RegisterAsync(RegisterRequest request);
    }
}