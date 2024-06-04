using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        /// <inheritdoc/>

        public bool AddReview(Review review)
        {
            _context.Reviews.Add(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public IQueryable<Review> GetAllReviews(int bookId)
        {
            return _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.Reviewer)
                .Where(r => r.BookId == bookId);
        }

        public Review GetReview(string userId, int bookId)
        {
            return _context.Reviews.FirstOrDefault(r=> r.BookId == bookId && r.UserId == userId);
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            return Save();  
        }

        public IQueryable<Review> GetUserReviews(string userId)
        {
            return _context.Reviews.Where(r => r.UserId == userId);
        }
    }
}
