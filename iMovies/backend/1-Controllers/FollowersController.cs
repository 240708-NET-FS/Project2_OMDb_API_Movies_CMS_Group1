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

        if (result!=null)
        {
            // Successfully added follower
            var followerResponseDTO = new FollowerResponseDTO {
              
              FollowingRelationshipId = result.FollowingRelationshipId,
              UserId = result.FollowerUserId,
              FollowerUserId = result.FollowerUserId,
              CreatedAt = result.CreatedAt
              
              };
            
            return Ok(followerResponseDTO); 
        }
        
         // Return a JSON response with an error message
          var errorResponse = new
          {
              Message = "User cannot follow self or the same user more than once."
          };

        
        return BadRequest(errorResponse); // Follower relationship already exists or user not allowed to follow self
       
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
    public async Task<IActionResult> DeleteFollowingRelationship(int id)
    {
         
          var result = await _followerService.DeleteFollowingRelationshipAsync(id);

        if (result)
        {
            return Ok("Following Relationship deleted successfully.");
        }

        return NotFound("Following Relationship not found.");
      }



      [HttpGet("{userId}/following-with-movies")]
      public async Task<IActionResult> GetFollowingWithMovies(int userId)
       {
          var followingWithMovies = await _followerService.GetFollowingWithMoviesAsync(userId);

          if (followingWithMovies == null || !followingWithMovies.Any())
          {
              return NotFound("This user is not following anyone.");
          }

          return Ok(followingWithMovies);
      }
}
