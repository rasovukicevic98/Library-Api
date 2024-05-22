using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IAuthorRepository
    {
        bool ExistById(int id);
        Task<Author> GetByIdAsync(int id);
        Task<bool> DeleteByIdAsync(Author author);
        List<Author> GetAllAuthors();
        bool UpdateAuthor(Author author);
        bool CreateAuthor(Author author);
    }
}
