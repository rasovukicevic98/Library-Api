using CSharpFunctionalExtensions;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<Result<RegisterUserDto, IEnumerable<string>>> RegisterUser(RegisterUserDto user);
        Task<Result<RegisterUserDto, IEnumerable<string>>> RegisterLibrarian(RegisterUserDto user);
        Task<Result<IEnumerable<string>>> Login(LoginUser loginUser);
        Task<string> GenerateTokenString(LoginUser loginUser);
    }
}