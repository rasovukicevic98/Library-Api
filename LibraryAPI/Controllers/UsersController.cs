using AutoMapper;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userRepository;
        private readonly IBookRentService _bookRentService;

        public UsersController(IAuthenticationService authService, UserManager<User> userManager,IMapper mapper, IUserService userRepository, IBookRentService bookRentService)
        {
            _authService = authService;           
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _bookRentService = bookRentService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Register(RegisterUserDto user)
        {
            var exist = await _userManager.FindByEmailAsync(user.Email);
            if (exist != null)
            {
                return BadRequest("Email is already taken!");
            }
            var result = await _authService.RegisterUser(user);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.IsSuccess);
        }

        /// <summary>
        /// Registers a new librarian.
        /// </summary>
        [HttpPost("register-librarian")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.Admin)]
        public async Task<IActionResult> RegisterLibrarian(RegisterUserDto user)
        {
            var exist =await _userManager.FindByEmailAsync(user.Email);
            if (exist != null)
            {
                return BadRequest("Email is already taken!");
            }
            var result = await _authService.RegisterLibrarian(user);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userRepository.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var result = await _userRepository.GetUserAsync(id);
            if (result!=null)
            {
                return Ok(result);
            }
            return NotFound("There is no user with id: "+id);            
        }

        /// <summary>
        /// Returns renting history for loged-in user.
        /// </summary>
        [HttpGet("users/{id}/return")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = LibraryRoles.User)]
        public async Task<IActionResult> RentHistory(string id)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookRentService.GetUserHistoryByEmailAsync(email);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

    }
}
