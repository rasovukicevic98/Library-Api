using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<Microsoft.AspNetCore.Identity.IdentityResult> AssignLibrarianRoleAsync(User user);
        Task<Microsoft.AspNetCore.Identity.IdentityResult> AssignUserRoleAsync(User user);
        Task<IdentityResult> RegisterNewUserAsync(User user, string password);
    }
}
