using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BookRentRepository : IBookRentRepository
    {
        private readonly DataContext _context;

        public BookRentRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddRent(BookRent bookRent)
        {
            _context.BookRents.Add(bookRent);
            return Save();
        }

        public BookRent GetActiveRent(int bookId, string userId)
        {
            return _context.BookRents
                .Include(br => br.Book)
                .Include(br => br.User)
                .FirstOrDefault(br => br.BookId == bookId && br.UserId.Equals(userId) && br.ReturnDate == null);
        }

        public bool UpdateBook(BookRent updateBook)
        {
            _context.BookRents.Update(updateBook);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
