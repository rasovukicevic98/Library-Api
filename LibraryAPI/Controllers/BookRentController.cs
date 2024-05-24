using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class BookRentController : ControllerBase
    {
        private readonly IBookRentService _bookRentService;

        public BookRentController(IBookRentService bookRentService)
        {
            _bookRentService = bookRentService;
        }

        /// <summary>
        /// Creates a new book rent.
        /// </summary>
        [HttpPost("users/{id}/rent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Rent(string id, BookRentDto bookRentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _bookRentService.RentBook(id, bookRentDto);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Return a book.
        /// </summary>
        [HttpPost("users/{id}/return")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Return(string id, BookRentDto bookRentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result =await  _bookRentService.ReturnABook(id, bookRentDto);
            if(result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Returns renting history for selected user.
        /// </summary>
        [HttpGet("users/{id}/rent-history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> RentHistory(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookRentService.GetUserHistory(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

    }
}
