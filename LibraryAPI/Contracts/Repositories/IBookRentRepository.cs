using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IBookRentRepository
    {
        bool AddRent(BookRent bookRent);
        BookRent GetRentedBooks(int bookId, string userId);
        List<BookRent> GetRents(int bookId);
        bool UpdateBook(BookRent bookRent);
        List<BookRent> GetUserRentHistory(string userId);
        List<BookRent> GetBookRentHistory(int bookId);
    }
}
