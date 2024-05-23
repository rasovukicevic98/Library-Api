using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> GetUsersAsync();
    }
}
