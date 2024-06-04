using CSharpFunctionalExtensions;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book GetById(int id);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        public bool ExistById(int id);        
    }
}
