using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Services;
using OMDbProject.Services.Interfaces;
using OMDbProject.Models.DTOs;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FollowersController : ControllerBase
{
    private readonly IFollowerService _followerService;

    public FollowersController(IFollowerService followerService)
    {
        _followerService = followerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFollower(FollowerDTO followerDTO)
    {
        var result = await _followerService.AddFollowerAsync(followerDTO);

        if (result)
        {
            return Ok(); // Successfully added follower
        }
        
        return BadRequest("You are already following this user."); // Follower relationship already exists
       
      }



    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFollowers(int userId)
    {
           var followers = await _followerService.GetFollowersByUserIdAsync(userId);

            if (followers == null || !followers.Any())
            {
              return NotFound("No followers found.");
            }

          return Ok(followers);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFollower(int id)
    {
          await Task.CompletedTask; // Placeholder for await
      return Ok();    
      }
}
