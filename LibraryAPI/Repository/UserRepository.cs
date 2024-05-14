using LibraryAPI.Models;
using LibraryAPI.Services;

namespace LibraryAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers()
        {
            var users = new List<User>();
            users.Add(new User {
                Name = "RASO",
                Email="raso",
                Id = "1",
                Password="raso"
            });

            return users;
        }
    }
}
