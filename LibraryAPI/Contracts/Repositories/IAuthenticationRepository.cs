using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<Microsoft.AspNetCore.Identity.IdentityResult> RegisterNewUserAsync(User user);
        Task<Microsoft.AspNetCore.Identity.IdentityResult> AssignLibrarianRoleAsync(User user);
        Task<Microsoft.AspNetCore.Identity.IdentityResult> AssignUserRoleAsync(User user);
    }
}
