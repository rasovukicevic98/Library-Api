                                                                                                                                                                using AutoMapper;
using LibraryAPI.Constants;
using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = LibraryRoles.User)]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IReviewService _reviewService;

        public ProfileController(IProfileService profileService, IReviewService reviewService)
        {
            _profileService = profileService;
            _reviewService = reviewService;
        }

        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> UpdatePassword(UpdateUserDto updateUser)
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
        public async Task<IActionResult> Update(UpdateProfileDto updateProfile)
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

        /// <summary>
        /// Returns all reviews loged-in user posted.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserReviews()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var result =await _reviewService.GetUserReviews(email);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

    }
}
