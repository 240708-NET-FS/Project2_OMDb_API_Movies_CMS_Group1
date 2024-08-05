using Microsoft.AspNetCore.Mvc;
using OMDbProject.Services.Interfaces;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly LikeService _likeService;

    public LikesController(LikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpPost]
    public async Task<IActionResult> AddLike(LikeDTO likeDTO)
    {
            return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpGet("usermovies/{userMovieId}")]
    public async Task<IActionResult> GetLikesForUserMovie(int userMovieId)
    {
          return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(int id)
    {
         return Ok();
    await Task.CompletedTask; // Placeholder for await
    }
}
