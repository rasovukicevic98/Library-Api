using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}
