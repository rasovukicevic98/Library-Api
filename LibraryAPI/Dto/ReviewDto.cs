using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class ReviewDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
