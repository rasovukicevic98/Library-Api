using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Repository;
using Microsoft.AspNet.Identity;
using System.Net;

namespace LibraryAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IProfileRepository profileRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _profileRepository = profileRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Result<ReviewDto, IEnumerable<string>>> CreateReview(ReviewDto reviewDto, string email)
        {
            var user = await _profileRepository.FindByEmailAsync(email);
            if (user == null || email == null)
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "There is no user with provided email." });
            }

            var book = _bookRepository.GetById(reviewDto.BookId);
            if (book == null)
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "There is no book with id:" + reviewDto.BookId });
            }

            Review review = _mapper.Map<Review>(reviewDto);
            review.UserId = user.Id;
            if (!_reviewRepository.AddReview(review))
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "Something went wrong during saving the review." });
            }

            return Result.Success<ReviewDto, IEnumerable<string>>(reviewDto);
        }

        public async Task<Result<List<ReviewDto>, IEnumerable<string>>> GetBookReviews(int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book == null)
            {
                return Result.Failure<List<ReviewDto>, IEnumerable<string>>(new List<string> { "There is no book with id: " + bookId });
            }
            var reviews = _reviewRepository.GetAllReviews(bookId).ToList();

            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);
            return Result.Success<List<ReviewDto>, IEnumerable<string>>(reviewsDto);
        }

        public async Task<Result<ReviewDto, IEnumerable<string>>> UpdateReview(ReviewDto reviewDto, string email)
        {
            var user = await _profileRepository.FindByEmailAsync(email);
            if (user == null || email == null)
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "There is no user with provided email." });
            }

            var book = _bookRepository.GetById(reviewDto.BookId);
            if (book == null)
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "There is no book with id:" + reviewDto.BookId });
            }

            var review = _reviewRepository.GetReview(user.Id, reviewDto.BookId);
            if (review == null)
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "You have no reviews for " + book.Title });
            }
            _mapper.Map(reviewDto, review);

            if (!await _reviewRepository.UpdateReview(review))
            {
                return Result.Failure<ReviewDto, IEnumerable<string>>(new List<string> { "Something went wrong while updating the review." });
            }

            return Result.Success<ReviewDto, IEnumerable<string>>(reviewDto);
        }

        public async Task<Result<List<ReviewDto>, IEnumerable<string>>> GetUserReviews(string email)
        {
            var user = await _profileRepository.FindByEmailAsync(email);
            if (user == null || email == null)
            {
                return Result.Failure<List<ReviewDto>, IEnumerable<string>>(new List<string> { "There is no user with provided id." });
            }

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetUserReviews(user.Id).ToList());

            return Result.Success<List<ReviewDto>, IEnumerable<string>>(reviews);
        }

    }
}
