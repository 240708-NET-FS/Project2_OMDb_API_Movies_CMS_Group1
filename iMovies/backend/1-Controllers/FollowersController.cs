using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models.DTOs;
using OMDBProject.Services;


[ApiController]
[Route("api/[controller]")]
public class FollowersController : ControllerBase
{
    private readonly FollowersService _followersService;

    public FollowersController(FollowersService followersService)
    {
        _followersService = followersService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFollower(FollowerDTO followerDto)
    {
     
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFollowers(int userId)
    {
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFollower(int id)
    {
        
    }
}
