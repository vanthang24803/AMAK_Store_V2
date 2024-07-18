using AMAK.Application.Dtos.Auth;
using AMAK.Application.Interfaces;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AMAK.Application.Services {
    public class AuthService : IAuthService {

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(RegisterRequest request) {


            var newUser = new ApplicationUser() {
                Email = request.Email,
                UserName = request.Username,
            };

            await _userManager.CreateAsync(newUser);

            return $"Create new user {newUser.UserName}";
        }
    }
}