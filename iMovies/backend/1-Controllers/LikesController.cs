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
await Task.CompletedTask; // Placeholder for await
      return Ok();    }

    [HttpGet("usermovies/{userMovieId}")]
    public async Task<IActionResult> GetLikesForUserMovie(int userMovieId)
    {
          await Task.CompletedTask; // Placeholder for await
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(int id)
    {
      await Task.CompletedTask; // Placeholder for await
      return Ok();   }
}
