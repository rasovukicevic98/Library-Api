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
    
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(IAuthService authService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
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
        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RegisterUser( User user)
        {
            var result =  await _authService.RegisterUser(user);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.IsSuccess); 
        }
        
        [HttpPost("Login")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result =await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);
            if(result.Succeeded)
            {
                var tokenString = _authService.GenerateTokenString(loginUser);
                return Ok(tokenString);
            }
            return BadRequest("Login attempt was unsuccessful!");
        }
    }
}
