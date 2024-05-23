using System.ComponentModel.DataAnnotations;
using LibraryAPI.Dto;

namespace LibraryAPI.Models
{
    public class BookRent
    {
        [Key]
        public int Id;
        public int BookId { get; set; }            
        public string UserId { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
