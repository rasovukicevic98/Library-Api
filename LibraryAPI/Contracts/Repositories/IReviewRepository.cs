using LibraryAPI.Models;

namespace LibraryAPI.Contracts.Repositories
{
    public interface IReviewRepository
    {
        bool AddReview(Review review);
        IQueryable<Review> GetAllReviews(int bookId);
        Review GetReview(string userId, int bookId);
        Task<bool> UpdateReview(Review review);
        IQueryable<Review> GetUserReviews(string userId);
    }
}
