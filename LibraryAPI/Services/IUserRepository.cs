using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IUserRepository
    {        
        bool RegisterUser(User user);
        bool Save();
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserAsync(string id);
    }
}
