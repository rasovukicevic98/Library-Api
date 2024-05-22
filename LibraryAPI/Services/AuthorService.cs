using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Migrations;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {        
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<Result<IEnumerable<string>>> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return Result.Failure<IEnumerable<string>>("There is no author with id: " + id);
            }            
            await _authorRepository.DeleteByIdAsync(author);
            
            return Result.Success(Enumerable.Empty<string>());
        }

        public List<AuthorDto> GetAllAuthors()
        {            
            return _mapper.Map<List<AuthorDto>>(_authorRepository.GetAllAuthors()); ;
        }

        public async Task<Result<AuthorDto, IEnumerable<string>>> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return Result.Failure<AuthorDto, IEnumerable<string>>(new List<string> { "There are no authors with id:" + id });
            }
            AuthorDto authorDto = _mapper.Map<AuthorDto>(author);
            return Result.Success<AuthorDto, IEnumerable<string>>(authorDto);
        }

        
        public async Task<Result<AuthorDto, IEnumerable<string>>> UpdateAuthor(int id, AuthorDto authorDto)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return Result.Failure<AuthorDto, IEnumerable<string>>(new List<string> { "There are no authors with id:" + id });
            }
            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.YearOfBirth = authorDto.YearOfBirth;
            _authorRepository.UpdateAuthor(author);
            
            AuthorDto returnDto = _mapper.Map<AuthorDto>(author);
            return Result.Success<AuthorDto, IEnumerable<string>>(returnDto);
        }

        public async Task<Result<IEnumerable<string>>> AddAuthor(AuthorDto authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);
            author.CreatedDate = DateTime.Now;
            author.IsDeleted = false;
            _authorRepository.CreateAuthor(author);
            
            return Result.Success(Enumerable.Empty<string>());
        }                
    }

}
