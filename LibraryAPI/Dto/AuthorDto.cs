using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class AuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearOfBirth { get; set; }        
    }
}
