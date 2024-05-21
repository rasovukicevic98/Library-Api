using CSharpFunctionalExtensions;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IAuthorRepository
    {
        Task<Result<List<Author>, IEnumerable<string>>> GetAllAuthors();

        Task<Result<User, IEnumerable<string>>> GetAuthorById(int id);
        Task<Result<IEnumerable<string>>> AddAuthor(Author author);
        Task<Result<IEnumerable<string>>> UpdateAuthor(Author author);
        Task<Result<IEnumerable<string>>> DeleteAuthor(Author author);
    }
}
