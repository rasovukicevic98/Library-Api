using CSharpFunctionalExtensions;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Services
{
    public interface IUserService
    {

        Task<List<LoginUserDto>> GetUsersAsync();
        Task<LoginUserDto> GetUserAsync(string id);

    }
}
