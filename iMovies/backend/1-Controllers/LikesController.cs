using Microsoft.AspNetCore.Mvc;
using OMDbProject.Services.Interfaces;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost]
    public async Task<IActionResult> AddLike(LikeDTORequest likeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _likeService.AddLikeAsync(likeDTO);

        if (result)
        {
            return Ok(result); // Successfully added like
        }

        return BadRequest("Unable to add like.");

    }


    [HttpGet("usermovies/{userMovieId}")]
    public async Task<IActionResult> GetLikesForUserMovie(int userMovieId)
    {
            var likes = await _likeService.GetLikesForUserMovieAsync(userMovieId);

            if (likes == null || !likes.Any())
            {
                return NotFound("No likes found for this movie.");
            }   

            return Ok(likes);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(int id)
    {
     
            var result = await _likeService.DeleteLikeAsync(id);

             if (result)
            {
                return Ok(result+ ":Like delete operation success"); // Successfully deleted like
            }

            return NotFound("Like not found.");
     
    }
}
