using CSharpFunctionalExtensions;
using LibraryAPI.Dto;

namespace LibraryAPI.Contracts.Services
{
    public interface IReviewService
    {
        Task<Result<ReviewDto, IEnumerable<string>>> CreateReview(ReviewDto reviewDto, string email);
        Task<Result<List<ReviewDto>, IEnumerable<string>>> GetBookReviews(int bookId);
        Task<Result<ReviewDto, IEnumerable<string>>> UpdateReview(ReviewDto reviewDto, string email);
        Task<Result<List<ReviewDto>, IEnumerable<string>>> GetUserReviews(string email);
    }
}
