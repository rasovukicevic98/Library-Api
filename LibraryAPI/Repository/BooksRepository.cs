using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace LibraryAPI.Repository
{
    public class BooksRepository : IBookRepository
    {
        private readonly DataContext _db;

        public BooksRepository(DataContext db)
        {
            _db = db;
        }

        public bool Add(Book book)
        {

            _db.Books.Add(book);
            return Save();
        }

        public bool Delete(Book book)
        {
            book.IsDeleted = true;
            return Save();
        }

        public List<Book> GetAll()
        {
            return _db.Books.ToList();
        }

        public Book GetById(int id)
        {
            var book = _db.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == id);
            return book;
        }

        public bool Update(Book book)
        {
            _db.Books.Update(book);
            return Save();
        }

        public bool ExistById(int id)
        {
            var res = _db.Books.FirstOrDefault(b => b.Id == id);
            if (res == null)
            {
                return false;
            }
            return true;
        }


        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
