using CSharpFunctionalExtensions;
using LibraryAPI.Dto;

namespace LibraryAPI.Contracts.Services
{
    public interface IUserService
    {
        Task<List<LoginUserDto>> GetUsersAsync();
        Task<LoginUserDto> GetUserAsync(string id);

    }
}
