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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Register(User user)
        {
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
        [ProducesResponseType(200)]
        [Authorize(Roles = LibraryRoles.Admin)]
        public async Task<IActionResult> RegisterLibrarian(User user)
        {
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
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userRepository.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
