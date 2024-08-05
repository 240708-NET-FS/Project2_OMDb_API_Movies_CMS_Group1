using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models.DTOs;
using OMDBProject.Services;
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly LikesService _likesService;

    public LikesController(LikesService likesService)
    {
        _likesService = likesService;
    }

    [HttpPost]
    public async Task<IActionResult> AddLike(LikeDTO likeDto)
    {
       
    }

    [HttpGet("usermovies/{userMovieId}")]
    public async Task<IActionResult> GetLikesForUserMovie(int userMovieId)
    {
       
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(int id)
    {
    
    }
}
