using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            return Save();
        }

        public async Task<bool> DeleteByIdAsync(Author author)
        {            
            author.IsDeleted = true;
            _context.Authors.Update(author);
            return Save();
        }

        public bool ExistById(int id)
        {
            return _context.Authors.Any(a => a.Id == id && !a.IsDeleted);
        }

        public List<Author> GetAllAuthors()
        {
            var authors = _context.Authors.Where(a=>a.IsDeleted==false).ToList();
            return authors;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var res =await _context.Authors.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
            return res;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0    ;
        }

        public bool UpdateAuthor(Author author)
        {
            _context.Update(author);
            return Save();
        }
    }
}
