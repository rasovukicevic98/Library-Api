using CSharpFunctionalExtensions;
using LibraryAPI.Data;
using LibraryAPI.Models;
using LibraryAPI.Services;

namespace LibraryAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _dataContext;
        public AuthorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Result<IEnumerable<string>>> DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Author>, IEnumerable<string>>> GetAllAuthors()
        {
            var authors = _dataContext.Authors.ToList();
            if (authors.Count==0)
            {
                return await Task.FromResult(Result.Failure<List<Author>, IEnumerable<string>>(new List<string> { "No authors found." }));                
            }
            return await Task.FromResult(Result.Success<List<Author>, IEnumerable<string>>(authors));
        }

        public Task<Result<User, IEnumerable<string>>> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<string>>> AddAuthor(Author author)
        {
            var exist = _dataContext.Authors.Where(a => a.FirstName.Equals(author.FirstName.Trim()) && a.LastName.Equals(author.LastName.Trim()));
            if (exist != null)
            {
                return Result.Failure<IEnumerable<string>>("Author already exists!");
            }
            _dataContext.Authors.Add(author);
            _dataContext.SaveChanges();
            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }

        public Task<Result<IEnumerable<string>>> UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
