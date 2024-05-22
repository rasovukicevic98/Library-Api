using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IProfileRepository
    {
        Task<bool> CheckPasswordAsync(IdentityUser identityUser, string password);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<IdentityUser> FindByNameAsync(string name);
        Task<bool> UpdateUserAsync(IdentityUser user);
        Task<string> GeneratePasswordResetTokenAsync(IdentityUser identityUser);
        Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string token, string password);
    }
}
