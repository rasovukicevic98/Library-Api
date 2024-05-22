using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Constants;
using LibraryAPI.Contracts;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _booksRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository booksRepository, IMapper mapper, IAuthorRepository authorRepository)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;

        }

        public async Task<Result<IEnumerable<string>>> AddBookAsync(BooksDto bookDto)
        {
            Book book = new Book(bookDto);
            book.Authors = new List<Author>();
            foreach (int author in bookDto.Authors)
            {
                
                var authorExist = await _authorRepository.GetByIdAsync(author);
                if (authorExist != null)
                {
                    book.Authors.Add(authorExist);
                }
                else
                {
                    string error = "One or more authors doesn't exist.";
                    return Result.Failure<IEnumerable<string>>(error);
                }
            }
            

            if (!_booksRepository.Add(book))
            {
                return Result.Failure<IEnumerable<string>>("There has been an error while saving the book.");
            }
            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }

        public async Task<Result<IEnumerable<string>>> DeleteBookAsync(int id)
        {
            var book = _booksRepository.GetById(id);
            if(book == null)
            {
                return Result.Failure<IEnumerable<string>>("There is no book with id:"+id);
            }
            var res = _booksRepository.Delete(book);            
            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }

        public async Task<List<BooksDto>> GetAllBooksAsync()
        {
            var books = _booksRepository.GetAll().ToList();

            List<BooksDto> booksDto = _mapper.Map<List<BooksDto>>(books);

            return booksDto;
        }

        public async Task<Result<BooksDto, IEnumerable<string>>> GetBookByIdAsync(int id)
        {
            var book = _booksRepository.GetById(id);
            if (book == null)
            {
                return Result.Failure<BooksDto, IEnumerable<string>>(new List<string> { "There is no book with " + id + " id" });
            }
            BooksDto bookDto = _mapper.Map<BooksDto>(book);
            return Result.Success<BooksDto, IEnumerable<string>>(bookDto);
        }

        public async Task<Result<IEnumerable<string>>> UpdateBookAsync(int id, BooksDto bookDto)
        {
            Book book = _booksRepository.GetById(id);
            book.Title = bookDto.Title;
            book.YearPublished = bookDto.YearPublished;
            book.ISBN = bookDto.ISBN;
            book.Genre = bookDto.Genre;
            book.NumberOfPages = bookDto.NumberOfPages;
            book.TotalCopies = bookDto.TotalCopies;

            book.Authors = new List<Author>();
            foreach (int author in bookDto.Authors)
            {
                var authorExist =await _authorRepository.GetByIdAsync(author);
                if (authorExist!=null)
                {                    
                    book.Authors.Add(authorExist);
                }
                else
                {
                    string error = "One or more authors doesn't exist.";
                    return Result.Failure<IEnumerable<string>>(error);
                }
            }

            _booksRepository.Update(book);
            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }
    }
}
