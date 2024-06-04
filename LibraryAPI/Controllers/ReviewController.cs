using LibraryAPI.Contracts.Services;
using LibraryAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewDto reviewDto)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var result =await _reviewService.CreateReview(reviewDto, email);
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ReviewDto reviewDto)
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var result = await _reviewService.UpdateReview(reviewDto, email);

            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

    }
}
