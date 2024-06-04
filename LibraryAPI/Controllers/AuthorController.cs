using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Data;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorRepository;

        public AuthorController(IAuthorService authorRepository)
        {
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// Returns an Author with specific id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthors(int id)
        {
            var res = await _authorRepository.GetAuthorById(id);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return NotFound(res.Error);
        }


        /// <summary>
        /// Returns all Authors.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthors()
        {
            var result = _authorRepository.GetAllAuthors();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new Author.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       // [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Post(AuthorDto author)
        {
            var result =await _authorRepository.AddAuthor(author);
            if(result.IsSuccess)
            {
                return Ok("Author successfully created");
            }
            return BadRequest(result.Error);            
        }

        /// <summary>
        /// Updates an existing Author.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, AuthorDto authorDto)
        {
            var res = await _authorRepository.UpdateAuthor(id, authorDto);
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }
            return NotFound(res.Error);
        }

        /// <summary>
        /// Deletes an Author
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var res =await _authorRepository?.DeleteAuthor(id);
            if (res.IsSuccess)
            {
                return Ok("Deleted successfully.");
            }
            return NotFound(res.Error);
        }
                
    }
}
