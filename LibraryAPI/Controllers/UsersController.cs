using AutoMapper;
using LibraryAPI.Constants;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UsersController(IAuthService authService, UserManager<IdentityUser> userManager,IMapper mapper, IUserRepository userRepository)
        {
            _authService = authService;           
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Register(User user)
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
        public async Task<IActionResult> RegisterLibrarian(User user)
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
    }
}
