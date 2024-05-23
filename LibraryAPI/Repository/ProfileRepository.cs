using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Migrations;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserManager<User> _userManager;

        public ProfileRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(User identityUser, string password)
        {
           return await _userManager.CheckPasswordAsync(identityUser, password);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindByNameAsync(string name)
        {
            return await _userManager.FindByEmailAsync(name);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User identityUser)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(identityUser);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            IdentityResult res = await _userManager.UpdateAsync(user);
            return res.Succeeded;
            
        }        
    }
}
