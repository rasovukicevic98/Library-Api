using LibraryAPI.Constants;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(IAuthService authService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _authService = authService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        ///<remarks>
        ///Role can only be "User" or "Librarian".
        /// </remarks>
        [HttpPost("register-user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = LibraryRoles.Librarian)]
        public async Task<IActionResult> Register( User user)
        {
            var result =  await _authService.RegisterUser(user);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.IsSuccess); 
        }

        /// <summary>
        /// Registers a new librarian.
        /// </summary>
        ///<remarks>
        ///Role can only be "User" or "Librarian".
        /// </remarks>
        /// <response code="200">Returns users token.</response>
        [HttpPost("register-librarian")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = LibraryRoles.Admin)]
        public async Task<IActionResult> RegisterLibrarian(User user)
        {
            var result = await _authService.RegisterLibrarian(user);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.IsSuccess);
        }
    }
}
