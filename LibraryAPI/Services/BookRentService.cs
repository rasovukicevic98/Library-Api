using AutoMapper;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryAPI.Services
{
    public class BookRentService : IBookRentService
    {
        private readonly IMapper _mapper;
        private readonly IBookRentRepository _bookRentRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<User> _userManager;

        public BookRentService(IMapper mapper, IBookRentRepository bookRentRepository, IBookRepository bookRepository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _bookRentRepository = bookRentRepository;
            _bookRepository = bookRepository;
            _userManager = userManager;
        }

        public async Task<Result<IEnumerable<string>>> RentBook(string userId, BookRentDto bookRentDto)
        {
            BookRent bookRent = _mapper.Map<BookRent>(bookRentDto);
            if (!_bookRepository.ExistById(bookRentDto.BookId))
            {
                return Result.Failure<IEnumerable<string>>("There is no book with " + bookRentDto.BookId + " id.");
            }
            if(!_userManager.Users.Any(U=>U.Id.Equals(userId)))
            {
                return Result.Failure<IEnumerable<string>>("There is no user with " + userId + " id.");
            }
            User user = await _userManager.FindByIdAsync(userId);
            if(!await _userManager.IsInRoleAsync(user, LibraryRoles.User))
            {
                return Result.Failure<IEnumerable<string>>("You can't rent a book to librarians or admin!");
            }
            
            Book book = _bookRepository.GetById(bookRentDto.BookId);
            if (book.TotalCopies <= 0)
            {
                return Result.Failure<IEnumerable<string>>("There are no book available with id: " + bookRentDto.BookId + ".");
            }
            bookRent.RentDate = DateTime.Now;
            bookRent.DueDate = DateTime.Now.AddDays(365);
            bookRent.UserId = userId;
            
            bool created = _bookRentRepository.AddRent(bookRent);
            if (!created)
            {
                return Result.Failure<IEnumerable<string>>("Error while creating the rent.");
            }
            book.TotalCopies--;
            _bookRepository.Update(book);
            return Result.Success<IEnumerable<string>>(new List<string> { "Successfully rented the book." });
        }

        public async Task<Result<IEnumerable<string>>> ReturnABook(string userId, BookRentDto bookReturnDto)
        {
            
            if (!_bookRepository.ExistById(bookReturnDto.BookId))
            {
                return Result.Failure<IEnumerable<string>>("There is no book with " + bookReturnDto.BookId + " id.");
            }
            if (!_userManager.Users.Any(U => U.Id.Equals(userId)))
            {
                return Result.Failure<IEnumerable<string>>("There is no user with " + userId + " id.");
            }
            BookRent bookRent = _bookRentRepository.GetRentedBooks(bookReturnDto.BookId, userId);
            if (bookRent == null)
            {
                return Result.Failure<IEnumerable<string>>("There is no active  rent with user id:" + userId + " and book id: " + bookReturnDto.BookId + ".");
            }
            if (bookReturnDto.Date < bookRent.RentDate)
            {
                return Result.Failure<IEnumerable<string>>("Invalid date.");
            }
            bookRent.ReturnDate = bookReturnDto.Date;
            if (!_bookRentRepository.UpdateBook(bookRent))
            {
                return Result.Failure<IEnumerable<string>>("Something went wrong while returning the book");
            }
            Book book = bookRent.Book;
            book.TotalCopies++;
            _bookRepository.Update(book);
            return Result.Success<IEnumerable<string>>(new List<string> { "Successfully returned the book." });
        }

        public async Task<Result<List<BookRentHistoryDto>,IEnumerable<string>>> GetUserHistory(string userId)
        {
            if (!_userManager.Users.Any(U => U.Id.Equals(userId)))
            {
                return Result.Failure< List < BookRentHistoryDto > ,IEnumerable<string>>(new List<string> { "There is no user with " + userId + " id." });
            }
            
            var result = _mapper.Map<List<BookRentHistoryDto>>(_bookRentRepository.GetUserRentHistory(userId));
            return Result.Success<List<BookRentHistoryDto>, IEnumerable<string>>(result);
        }

        public async Task<Result<List<BookRentHistoryDto>, IEnumerable<string>>> GetUserHistoryByEmailAsync(string userEmail)
        {
            User user = await _userManager.FindByEmailAsync(userEmail);
                       
            var result = _mapper.Map<List<BookRentHistoryDto>>(_bookRentRepository.GetUserRentHistory(user.Id));
            return Result.Success<List<BookRentHistoryDto>, IEnumerable<string>>(result);
        }

        public async Task<Result<List<BookRentHistoryDto>, IEnumerable<string>>> GetBookHistory(int bookId)
        {
            List<BookRentHistoryDto> bookRentHistoryDtos = _mapper.Map<List<BookRentHistoryDto>>(_bookRentRepository.GetRents(bookId));
                        
            return Result.Success<List<BookRentHistoryDto>, IEnumerable<string>>(bookRentHistoryDtos);
        }
    }
}
