using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class BookRentDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        
    }
}
