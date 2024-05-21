using AutoMapper;
using CSharpFunctionalExtensions;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Migrations;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public AuthorRepository(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<string>>> DeleteAuthor(int id)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {               
                return Result.Failure<IEnumerable<string>>("There is no author with id: "+id);
        }
            _dataContext.Authors.Remove(author);
            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());

        }

        public List<AuthorDto> GetAllAuthors()
        {
            List<AuthorDto> authors = _mapper.Map<List<AuthorDto>>(_dataContext.Authors.ToList());

            return authors;
        }

        public async Task<Result<AuthorDto, IEnumerable<string>>> GetAuthorById(int id)
            {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if(author == null)
            {
                return Result.Failure<AuthorDto, IEnumerable<string>>(new List<string> { "There are no authors with id:" + id });
            }
            AuthorDto authorDto = _mapper.Map<AuthorDto>(author);
            return Result.Success<AuthorDto, IEnumerable<string>>(authorDto);
        }

        public async Task<Result<AuthorDto, IEnumerable<string>>> UpdateAuthor(int id, AuthorDto authorDto)
        {
            var exist = await _dataContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
        {
                return Result.Failure<AuthorDto, IEnumerable<string>>(new List<string> { "There are no authors with id:" + id });
            }
            exist.FirstName=authorDto.FirstName;
            exist.LastName=authorDto.LastName;
            exist.YearOfBirth = authorDto.YearOfBirth;
            _dataContext.Update(exist);
            _dataContext.SaveChanges();
            AuthorDto returnDto = _mapper.Map<AuthorDto>(exist);
            return Result.Success<AuthorDto, IEnumerable<string>>(returnDto);
        }

        public async Task<Result<IEnumerable<string>>> AddAuthor(AuthorDto authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);
            author.CreatedDate = DateTime.Now;
            author.IsDeleted = false;

            var exist = await _dataContext.Authors.AnyAsync(a => a.FirstName.Equals(author.FirstName.Trim()) && a.LastName.Equals(author.LastName.Trim()));
            if (exist == true)
            {
                return Result.Failure<IEnumerable<string>>("Author already exists!");
            }
            _dataContext.Authors.Add(author);
            _dataContext.SaveChanges();
            return Result.Success<IEnumerable<string>>(Enumerable.Empty<string>());
        }
    }

}
