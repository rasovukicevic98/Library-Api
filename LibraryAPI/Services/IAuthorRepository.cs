using CSharpFunctionalExtensions;
using LibraryAPI.Dto;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IAuthorRepository
    {
        List<AuthorDto> GetAllAuthors();
        Task<Result<AuthorDto, IEnumerable<string>>> GetAuthorById(int id);
        Task<Result<IEnumerable<string>>> AddAuthor(AuthorDto author);
        Task<Result<AuthorDto, IEnumerable<string>>> UpdateAuthor(int id, AuthorDto author);
        Task<Result<IEnumerable<string>>> DeleteAuthor(int id);
    }
}
