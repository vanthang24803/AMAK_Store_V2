using System.IdentityModel.Tokens.Jwt;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Domain.Models;

namespace AMAK.Application.Interfaces {
    public interface ITokenService {
        TokenResponse GenerateToken(ApplicationUser user, IList<string> roles);
        JwtPayload DecodeRefreshToken(string token);
        string GenerateAccessToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken(ApplicationUser user, IList<string> roles);
    }
}