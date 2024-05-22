using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Migrations;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser identityUser, string password)
        {
           return await _userManager.CheckPasswordAsync(identityUser, password);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser> FindByNameAsync(string name)
        {
            return await _userManager.FindByEmailAsync(name);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(IdentityUser identityUser)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(identityUser);
        }

        public async Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<bool> UpdateUserAsync(IdentityUser user)
        {
            IdentityResult res = await _userManager.UpdateAsync(user);
            return res.Succeeded;
            
        }        
    }
}
