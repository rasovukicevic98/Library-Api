using LibraryAPI.Constants;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;


namespace LibraryAPI.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        public AuthenticationRepository(DataContext context, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityResult> RegisterNewUserAsync(User user, string password)
        {
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityResult> AssignLibrarianRoleAsync(User user)
        {
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.AddToRoleAsync(user, LibraryRoles.Librarian);
            return result;
        }
        public async Task<Microsoft.AspNetCore.Identity.IdentityResult> AssignUserRoleAsync(User user)
        {
            Microsoft.AspNetCore.Identity.IdentityResult result = await _userManager.AddToRoleAsync(user, LibraryRoles.User);
            return result;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
