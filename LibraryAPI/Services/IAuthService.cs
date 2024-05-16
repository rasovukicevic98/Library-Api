using CSharpFunctionalExtensions;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IAuthService
    {
        Task<Result<User, IEnumerable<string>>> RegisterUser(User user);
        Task<Result<bool,string>> Login(LoginUser loginUser);
        Task<string> GenerateTokenString(LoginUser loginUser);
    }
}