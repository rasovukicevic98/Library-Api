using LibraryAPI.Constants;
using LibraryAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Dto
{
    public class BooksDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "ISBN must have exactly 13 characters.")]
        [MinLength(13, ErrorMessage = "ISBN must have exactly 13 characters.")]
        public string ISBN { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "You must provide at least 1 author.")]
        public ICollection<int> Authors { get; set; }

        [Required]
        [EnumDataType(typeof(Genres), ErrorMessage = "Genre is not valid.")]
        public Genres Genre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of pages must be greater than 1.")]
        public int NumberOfPages { get; set; }

        [Range(1, 2024, ErrorMessage = "Number of pages must be greater than 1.")]
        public int YearPublished { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of copies must be greater than 1.")]
        public int TotalCopies { get; set; }
    }
}
