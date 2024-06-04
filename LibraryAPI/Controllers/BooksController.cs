using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using LibraryAPI.Mediatr.Requests;
using LibraryAPI.Repository;
using LibraryAPI.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBookRentService _bookRentService;
        private readonly IMediator _mediator;
        private readonly IReviewService _reviewService;

        public BooksController(IBookService bookService, IBookRentService bookRentService, IMediator mediator, IReviewService reviewService)
        {
            _bookService = bookService;
            _bookRentService = bookRentService;
            _mediator = mediator;
            _reviewService = reviewService;
        }

        /// <summary>
        /// Creates a new Book.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Post(BooksDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookService.AddBookAsync(bookDto);
            if (result.IsSuccess)
            {
                return Ok("Book successfully created");
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Returns all Books.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooks()
        {
            var result = _bookService.GetAllBooksAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a Book with specific id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBook(int id)
        {
            //MediatR practise.
            var res = await _mediator.Send(new GetBookByIdQuery(id));
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
            
            //var res = await _bookService.GetBookByIdAsync(id);
            //if (res.IsSuccess)
            //{
            //    return Ok(res.Value);
            //}
            //return NotFound(res.Error);
        }

        /// <summary>
        /// Deletes a Book
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _bookService.DeleteBookAsync(id);
            if (res.IsSuccess)
            {
                return Ok("Deleted successfully.");
            }
            return NotFound(res.Error);
        }

        /// <summary>
        /// Updates an existing Book.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Update(int id, BooksDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _bookService.UpdateBookAsync(id, bookDto);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return NotFound(res.Error);
        }

        /// <summary>
        /// Returns renting history for selected book.
        /// </summary>
        [HttpGet("{id}/rent-history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{LibraryRoles.Librarian},{LibraryRoles.Admin}")]
        public async Task<IActionResult> RentHistory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookRentService.GetBookHistory(id);

            return Ok(result.Value);
        }

        /// <summary>
        /// Returns review history for selected book.
        /// </summary>
        [HttpGet("{id}/review-history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> ReviewHistory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _reviewService.GetBookReviews(id);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
