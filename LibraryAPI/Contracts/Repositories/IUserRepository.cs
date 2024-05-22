using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetUserByIdAsync(string id);
        Task<List<IdentityUser>> GetUsersAsync();
    }
}
