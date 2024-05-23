using LibraryAPI.Constants;
using LibraryAPI.Dto;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public ICollection<Author> Authors { get; set; }
        public Genres Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int YearPublished { get; set; }
        public int TotalCopies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<BookRent> BookRents { get; set; }
        public Book(BooksDto bookDto)
        {
            Title = bookDto.Title;
            ISBN = bookDto.ISBN;
            Genre = bookDto.Genre;
            NumberOfPages = bookDto.NumberOfPages;
            YearPublished = bookDto.YearPublished;
            TotalCopies = bookDto.TotalCopies;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            IsDeleted = false;
        }
        public Book()
        {
            
        }
    }
}
