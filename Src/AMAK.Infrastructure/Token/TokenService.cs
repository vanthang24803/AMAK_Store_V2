using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace AMAK.Infrastructure.Token {
    public class TokenService : ITokenService {

        private readonly IConfiguration _configuration;

        private readonly SymmetricSecurityKey _authSecret;

        private readonly SymmetricSecurityKey _refreshSecret;


        public TokenService(IConfiguration configuration) {
            _configuration = configuration;
            _authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            _refreshSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Refresh"]!
            ));
        }

        public string GenerateAccessToken(ApplicationUser user, IList<string> roles, string provider) {
            var payload = ClaimsPayload(user, roles, provider);

            return GenerateToken(payload, _authSecret, DateTime.Now.AddMinutes(10));
        }

        public string GenerateRefreshToken(ApplicationUser user, IList<string> roles, string provider) {
            var payload = ClaimsPayload(user, roles, provider);

            return GenerateToken(payload, _refreshSecret, DateTime.Now.AddMonths(1));

        }

        public TokenResponse GenerateToken(ApplicationUser user, IList<string> roles, string provider) {
            var ac_token = this.GenerateAccessToken(user, roles, provider);
            var rf_token = this.GenerateRefreshToken(user, roles, provider);

            return new TokenResponse() {
                AccessToken = ac_token,
                RefreshToken = rf_token,
            };
        }

        private static List<Claim> ClaimsPayload(ApplicationUser user, IList<string> roles, string provider) {

            var claims = new List<Claim>() {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"),
                new("provider", provider)
            };

            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private string GenerateToken(
          List<Claim> claims, SymmetricSecurityKey key, DateTime expires
        ) {
            var tokenObject = new JwtSecurityToken(
                              issuer: _configuration["JWT:ValidIssuer"],
                              audience: _configuration["JWT:ValidAudience"],
                              expires: expires,
                              claims: claims,
                              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        public JwtPayload DecodeRefreshToken(string token) {
            return this.DecodeToken(token, _refreshSecret, false);
        }

        public JwtPayload DecodeAccessToken(string token) {
            return DecodeToken(token, _authSecret);
        }


        private JwtPayload DecodeToken(string token,
                                SymmetricSecurityKey key,
                                bool validateLifetime = true) {

            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = validateLifetime,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var payload = jwtToken.Payload;

                return payload;

            } catch {
                throw new UnauthorizedException();
            }
        }

        public SocialRequest DecodeSocialToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var exp = jwtToken.Payload.Claims.First(x => x.Type == "exp").Value;

            DateTimeOffset expTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp));

            if (expTime < DateTime.UtcNow) {
                throw new BadRequestException("Token is expires!");
            }

            var firebaseClaimValue = jwtToken.Payload.Claims.First(c => c.Type == "firebase").Value;

            var firebase = JObject.Parse(firebaseClaimValue);
            var provider = (firebase["sign_in_provider"]?.ToString()) ?? throw new BadRequestException("Provider is null");

            return new SocialRequest(
                jwtToken.Payload.Claims.First(c => c.Type == "email").Value,
                jwtToken.Payload.Claims.First(c => c.Type == "name").Value,
                jwtToken.Payload.Claims.First(c => c.Type == "picture").Value,
                provider
               );
        }
    }
}