using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<BookRent> BookRents { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
