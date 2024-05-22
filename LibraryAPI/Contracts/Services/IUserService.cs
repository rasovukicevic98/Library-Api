using CSharpFunctionalExtensions;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Services
{
    public interface IUserService
    {

        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserAsync(string id);

    }
}
