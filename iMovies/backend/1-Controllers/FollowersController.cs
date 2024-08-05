using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FollowersController : ControllerBase
{
    private readonly FollowerService _followerService;

    public FollowersController(FollowerService followerService)
    {
        _followerService = followerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFollower(FollowerDTO followerDTO)
    {
       return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFollowers(int userId)
    {
          return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFollower(int id)
    {
          return Ok();
    await Task.CompletedTask; // Placeholder for await
    }
}
