using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AMAK.Application.Services.Authentication {
    public class AuthService : IAuthService {

        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }


    }
}