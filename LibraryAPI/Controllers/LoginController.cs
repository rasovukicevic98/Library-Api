using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginController(IAuthService authService, SignInManager<IdentityUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);
            if (result.Succeeded)
            {
                var tokenString = await _authService.GenerateTokenString(loginUser);
                return Ok(tokenString);
            }
            return BadRequest("Login attempt was unsuccessful!");
        }
    }
}
