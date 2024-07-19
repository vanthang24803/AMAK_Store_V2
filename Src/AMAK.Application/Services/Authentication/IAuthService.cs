using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Authentication.Dtos;
using Microsoft.AspNetCore.Identity;

namespace AMAK.Application.Services.Authentication {
    public interface IAuthService {
        Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request);

        Task<List<IdentityRole>> GetRoles();

        Task<string> CreateSeedRole();
        
    }
}