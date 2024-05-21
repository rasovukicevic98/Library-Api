using CSharpFunctionalExtensions;
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
        Task<Result<UpdateUser, IEnumerable<string>>> UpdateUserPasswordAsync(string id, UpdateUser updateUser);
        Task<Result<IEnumerable<string>>> UpdateUsernameAsync(UpdateProfile updateProfile, string id);
    }
}
