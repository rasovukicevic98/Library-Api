using CSharpFunctionalExtensions;
using LibraryAPI.Dto;

namespace LibraryAPI.Contracts.Services
{
    public interface IBookService
    {
        Task<List<BooksDto>> GetAllBooksAsync();
        Task<Result<BooksDto, IEnumerable<string>>> GetBookByIdAsync(int id);
        Task<Result<IEnumerable<string>>> AddBookAsync(BooksDto bookDto);
        Task<Result<IEnumerable<string>>> UpdateBookAsync(int id, BooksDto bookDto);
        Task<Result<IEnumerable<string>>> DeleteBookAsync(int id);
                
    }
}
