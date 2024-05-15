using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        ///<remarks>
        ///Role can only be "User" or "Librarian".
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterUser( User user)
        {
            var result =  await _authService.RegisterUser(user);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.IsSuccess); 
        }
    }
}
