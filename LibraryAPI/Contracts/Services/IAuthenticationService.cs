using CSharpFunctionalExtensions;
using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<Result<User, IEnumerable<string>>> RegisterUser(User user);
        Task<Result<User, IEnumerable<string>>> RegisterLibrarian(User user);
        Task<Result<IEnumerable<string>>> Login(LoginUser loginUser);
        Task<string> GenerateTokenString(LoginUser loginUser);
    }
}