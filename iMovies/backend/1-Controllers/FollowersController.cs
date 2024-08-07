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
     await Task.CompletedTask; // Placeholder for await
      return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFollowers(int userId)
    {
      await Task.CompletedTask; // Placeholder for await
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFollower(int id)
    {
          await Task.CompletedTask; // Placeholder for await
      return Ok();    
      }
}
