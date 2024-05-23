using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IBookRentRepository
    {
        bool AddRent(BookRent bookRent);
        BookRent GetActiveRent(int bookId, string userId);
        bool UpdateBook(BookRent bookRent);
    }
}
