using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(User user);
    }
}