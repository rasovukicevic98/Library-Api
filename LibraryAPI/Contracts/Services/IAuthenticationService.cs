using CSharpFunctionalExtensions;
using LibraryAPI.Dto;

namespace LibraryAPI.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<Result<RegisterUserDto, IEnumerable<string>>> RegisterUser(RegisterUserDto user);
        Task<Result<RegisterUserDto, IEnumerable<string>>> RegisterLibrarian(RegisterUserDto user);
        Task<Result<IEnumerable<string>>> Login(LoginUserDto loginUser);
        Task<string> GenerateTokenString(LoginUserDto loginUser);
    }
}