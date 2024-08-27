using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Me.Dtos;
using Microsoft.AspNetCore.Identity;

namespace AMAK.Application.Services.Authentication {
    public interface IAuthService {
        Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request);

        Task<Response<TokenResponse>> LoginAsync(LoginRequest request);

        Task<Response<TokenResponse>> RefreshTokenAsync(TokenRequest request);

        Task<Response<TokenResponse>> VerifyAccountAsync(string userId, string token);

        Task<Response<string>> LogoutAsync(ClaimsPrincipal claims, TokenRequest token);

        Task<Response<string>> ResetPasswordAsync(string userId, string token, ResetPasswordRequest request);
        Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<Response<ProfileResponse>> UpgradeToManager(UpgradeRole upgrade);
        Task<Response<ProfileResponse>> UpgradeToAdmin(UpgradeRole upgrade);

        Task<Response<TokenResponse>> SignInWithGoogle(SocialLoginRequest request);

        Task<Response<AdminResponse>> CreateBotChatApp(CreateBotRequest request);

        Task<Response<List<AdminResponse>>> GetAllAdminMemberAsync();
        Task<string> CreateSeedRole();
        Task<List<IdentityRole>> GetRoles();
    }
}