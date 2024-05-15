using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<User,IEnumerable<string>>> RegisterUser(User user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.Name,
                Email = user.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false, 
                LockoutEnabled = false,
                AccessFailedCount = 0,
            };
            
            if (!await _roleManager.RoleExistsAsync(user.Role))
            {
                List<string> potentialErrors = new List<string>();
                string potentialError = "Role "+user.Role+ " does not exist!";
                potentialErrors.Add(potentialError);
                return Result.Failure<User, IEnumerable<string>>(potentialErrors);
            }
            
            var resultUser = await _userManager.CreateAsync(identityUser, user.Password);
            
            if (resultUser.Succeeded)
            {
                var  resultRole = await _userManager.AddToRoleAsync(identityUser, user.Role);
                return Result.Success<User, IEnumerable<string>>(user);
            }
            return Result.Failure<User, IEnumerable<string>>(resultUser.Errors.Select(e => e.Description));

        }
    }
}
