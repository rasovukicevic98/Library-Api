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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result =await _authService.Login(loginUser);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}
