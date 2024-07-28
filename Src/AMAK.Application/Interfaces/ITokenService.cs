using System.IdentityModel.Tokens.Jwt;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Domain.Models;

namespace AMAK.Application.Interfaces {
    public interface ITokenService {
        TokenResponse GenerateToken(ApplicationUser user, IList<string> roles, string provider);
        JwtPayload DecodeRefreshToken(string token);
        JwtPayload DecodeAccessToken(string token);
        SocialRequest DecodeSocialToken(string token);
        string GenerateAccessToken(ApplicationUser user, IList<string> roles, string provider);
        string GenerateRefreshToken(ApplicationUser user, IList<string> roles, string provider);
    }
}