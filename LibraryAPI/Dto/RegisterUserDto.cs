using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Dto
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
