using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Providers.Mail;
using AMAK.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using AMAK.Application.Services.Me.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

namespace AMAK.Application.Services.Authentication {
    public class AuthService : IAuthService {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMailService _mailService;

        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMailService mailService, IMapper mapper, ITokenService tokenService) {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request) {

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null) {
                throw new BadRequestException("Email existed!");
            }

            var newUser = _mapper.Map<ApplicationUser>(request);

            newUser.UserName = request.Email;
            newUser.SecurityStamp = Guid.NewGuid().ToString();


            var createUserResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!createUserResult.Succeeded) {
                throw new BadRequestException("Wrong Data!");
            }


            if (!await _roleManager.RoleExistsAsync(Role.CUSTOMER)) {
                throw new BadRequestException("Customer Role Not found!");
            }

            await _userManager.AddToRoleAsync(newUser, Role.CUSTOMER);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);


            _mailService.SendEmailConfirmationAccount(newUser.UserName, newUser.Id, token);


            var response = _mapper.Map<RegisterResponse>(newUser);


            return new Response<RegisterResponse>(HttpStatusCode.Created, response);
        }

        public async Task<Response<TokenResponse>> VerifyAccountAsync(string userId, string token) {
            var existingUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("Account not found!");
            var roles = await _userManager.GetRolesAsync(existingUser);

            var confirmAccount = await _userManager.ConfirmEmailAsync(existingUser, token);

            if (!confirmAccount.Succeeded) {
                throw new UnauthorizedException();
            }

            var response = _tokenService.GenerateToken(existingUser, roles);

            return new Response<TokenResponse>(HttpStatusCode.OK, response);
        }


        public async Task<Response<TokenResponse>> LoginAsync(LoginRequest request) {
            var existingUser = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException("Account not found!");

            if (!existingUser.EmailConfirmed) {
                throw new ForbiddenException();
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);

            if (!isPasswordCorrect) {
                throw new UnauthorizedException();
            }

            var userRoles = await _userManager.GetRolesAsync(existingUser);

            var token = _tokenService.GenerateToken(existingUser, userRoles);

            await _userManager.RemoveAuthenticationTokenAsync(existingUser, Provider.Account, Token.RefreshToken);

            await _userManager.SetAuthenticationTokenAsync(existingUser, Provider.Account, Token.RefreshToken, token.RefreshToken);

            return new Response<TokenResponse>(HttpStatusCode.OK, token);
        }


        public async Task<Response<TokenResponse>> RefreshTokenAsync(TokenRequest request) {

            var decode = _tokenService.DecodeRefreshToken(request.Token);

            var existingUser = await _userManager.FindByIdAsync(decode.Sub)
                ?? throw new NotFoundException("Account not found!");

            var existingRoles = await _userManager.GetRolesAsync(existingUser);

            var existingToken = await _userManager.GetAuthenticationTokenAsync(existingUser, Provider.Account, Token.RefreshToken)
                ?? throw new NotFoundException("Token not found!");

            if (!existingToken.Equals(request.Token)) {
                throw new BadRequestException("Token doesn't match!");
            }

            var accessToken = _tokenService.GenerateAccessToken(existingUser, existingRoles);

            var exp = DateTimeOffset.FromUnixTimeSeconds((long)decode.Expiration!).UtcDateTime;

            TokenResponse tokenResponse;

            if (exp >= DateTime.Now) {
                tokenResponse = new TokenResponse() {
                    AccessToken = accessToken,
                    RefreshToken = existingToken
                };
            } else {

                await _userManager.RemoveAuthenticationTokenAsync(existingUser, Provider.Account, Token.RefreshToken);

                var newRefreshToken = _tokenService.GenerateRefreshToken(existingUser, existingRoles);

                await _userManager.SetAuthenticationTokenAsync(existingUser, Provider.Account, Token.RefreshToken, newRefreshToken);

                tokenResponse = new TokenResponse() {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken
                };
            }

            return new Response<TokenResponse>(HttpStatusCode.OK, tokenResponse);
        }


        public async Task<Response<string>> LogoutAsync(ClaimsPrincipal claims) {

            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            await _userManager.RemoveAuthenticationTokenAsync(existingUser, Provider.Account, Token.RefreshToken);
            return new Response<string>(HttpStatusCode.OK, "Logout successfully!");
        }


        public async Task<List<IdentityRole>> GetRoles() {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<string> CreateSeedRole() {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(Role.MANAGER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(Role.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(Role.CUSTOMER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists) throw new BadRequestException("Roles Seeding is Already Done");


            await _roleManager.CreateAsync(new IdentityRole(Role.CUSTOMER));
            await _roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(Role.MANAGER));

            return "OK";
        }

        public async Task<Response<string>> ResetPasswordAsync(string userId, string token, ResetPasswordRequest request) {

            var existingUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("Account not found!");


            var updatePassword = await _userManager.ResetPasswordAsync(existingUser, token, request.NewPassword);

            if (!updatePassword.Succeeded) {
                throw new BadRequestException("Token doesn't match!");
            }

            return new Response<string>(HttpStatusCode.OK, "Reset password successfully!");
        }

        public async Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest request) {

            var existingUser = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException("Account not found!");

            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);


            _mailService.SendMailResetPassword(request.Email, existingUser.Id, token);


            return new Response<string>(HttpStatusCode.OK, "Send mail reset password successfully!");
        }

        public async Task<Response<ProfileResponse>> UpgradeToManager(UpgradeRole upgrade) {
            var existingUser = await _userManager.FindByEmailAsync(upgrade.Email) ?? throw new NotFoundException("Account not found!");

            await _userManager.AddToRoleAsync(existingUser, Role.MANAGER);

            var roles = await _userManager.GetRolesAsync(existingUser);

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }

        public async Task<Response<ProfileResponse>> UpgradeToAdmin(UpgradeRole upgrade) {
            var existingUser = await _userManager.FindByEmailAsync(upgrade.Email) ?? throw new NotFoundException("Account not found!");

            await _userManager.AddToRoleAsync(existingUser, Role.ADMIN);

            var roles = await _userManager.GetRolesAsync(existingUser);

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }

        public async Task<Response<TokenResponse>> SignInWithGoogle(SocialLoginRequest request) {

            var decoded = DecodeJwtToken(request.Token);

            if (!decoded.Provider.Equals(Provider.Google)) {
                throw new BadRequestException("Provider not suitable!");
            }

            var existingAccount = await _userManager.FindByEmailAsync(decoded.Email);

            if (existingAccount == null) {
                var newAccount = new ApplicationUser() {
                    Email = decoded.Email,
                    Avatar = decoded.Avatar,
                    FirstName = string.Empty,
                    LastName = decoded.Name,
                    UserName = decoded.Email,
                    EmailConfirmed = true,
                };

                var createUserResult = await _userManager.CreateAsync(newAccount);

                if (!createUserResult.Succeeded) {
                    throw new BadRequestException("Wrong data!");
                }

                if (!await _roleManager.RoleExistsAsync(Role.CUSTOMER)) {
                    throw new BadRequestException("Customer Role Not found!");
                }

                await _userManager.AddToRoleAsync(newAccount, Role.CUSTOMER);

                var token = await GenerateAndSetTokensAsync(newAccount);

                return new Response<TokenResponse>(HttpStatusCode.OK, token);
            } else {

                var token = await GenerateAndSetTokensAsync(existingAccount);

                return new Response<TokenResponse>(HttpStatusCode.OK, token);
            }
        }

        private async Task<TokenResponse> GenerateAndSetTokensAsync(ApplicationUser account) {
            var roles = await _userManager.GetRolesAsync(account);

            var token = _tokenService.GenerateToken(account, roles);

            await _userManager.RemoveAuthenticationTokenAsync(account, Provider.Account, Token.RefreshToken);

            await _userManager.SetAuthenticationTokenAsync(account, Provider.Account, Token.RefreshToken, token.RefreshToken);

            return token;
        }

        private static SocialRequest DecodeJwtToken(string token) {
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