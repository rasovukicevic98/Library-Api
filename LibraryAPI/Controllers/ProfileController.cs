using AutoMapper;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = LibraryRoles.User)]
        public async Task<IActionResult> UpdatePassword(UpdateUser updateUser)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var result = await _profileService.UpdateUserPasswordAsync(email, updateUser);
           

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (updateUser == null)
            {
                return BadRequest();
            }
            
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
            if (updateProfile == null)
            {
                return BadRequest();
            }
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var result = await _profileService.UpdateUserProfile(updateProfile, email);
            
            
            if (result.IsSuccess)
            {
                return Ok("You have successfully changed username");
            }
            return BadRequest(result.Error);
        }
    }
}
