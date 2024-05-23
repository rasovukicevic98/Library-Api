using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return  _userManager.Users.ToList();
        }
    }
}
