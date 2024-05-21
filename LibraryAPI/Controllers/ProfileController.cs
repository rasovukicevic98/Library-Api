using AutoMapper;
using LibraryAPI.Constants;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ProfileController(IAuthService authService, UserManager<IdentityUser> userManager, IMapper mapper, IUserRepository userRepository)
        {
            _authService = authService;
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.User)]
        public async Task<IActionResult> UpdatePassword( UpdateUser updateUser)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (updateUser == null)
            {
                return BadRequest();
            }            
            var result = await _userRepository.UpdateUserPasswordAsync(user.Id, updateUser);
            if (result.IsSuccess)
            {
                return Ok("You have successfully changed password");
            }
            return BadRequest(result.Error);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.User)]
        public async Task<IActionResult> Update(UpdateProfile updateProfile)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user=await _userManager.FindByEmailAsync(email);
            if (updateProfile == null )
            {
                return BadRequest();
            }
            var result = await _userRepository.UpdateUsernameAsync(updateProfile, user.Id);
            if (result.IsSuccess)
            {
                return Ok("You have successfully changed username");
            }
            return BadRequest(result.Error);
        }
    }
}
