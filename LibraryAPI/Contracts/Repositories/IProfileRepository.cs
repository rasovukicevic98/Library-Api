using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IProfileRepository
    {
        Task<bool> CheckPasswordAsync(User identityUser, string password);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByNameAsync(string name);
        Task<bool> UpdateUserAsync(User user);
        Task<string> GeneratePasswordResetTokenAsync(User identityUser);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    }
}
