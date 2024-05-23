using CSharpFunctionalExtensions;
using LibraryAPI.Dto;

namespace LibraryAPI.Contracts.Services
{
    public interface IBookRentService
    {
        Task<Result<IEnumerable<string>>> RentBook(string userId, BookRentDto bookRentDto);
        Task<Result<IEnumerable<string>>> ReturnABook(string userId, BookRentDto bookRentDto);
    }
}
