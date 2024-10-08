using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Models.DTOs;
using System.Threading.Tasks;

namespace OMDbProject.Services;
public class FollowerService : IFollowerService 
{
     private readonly IFollowerRepository _followerRepository;

        public FollowerService(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

    public async Task<Follower> AddFollowerAsync(FollowerDTO followerDTO)
        {

              // Check if the user is trying to follow themselves
                if (followerDTO.UserId == followerDTO.FollowerUserId)
                {
                   
                    return null; // or alternatively throw new InvalidOperationException("User cannot follow themselves.");
                
                    // throw new InvalidOperationException("User cannot follow self.");
                }
            
            var follower = new Follower
            {
                UserId = followerDTO.UserId,
                FollowerUserId = followerDTO.FollowerUserId,
                CreatedAt = DateTime.UtcNow
            };

            return await _followerRepository.AddFollowerAsync(follower);
        }


public async Task<IEnumerable<FollowerDTO>> GetFollowersByUserIdAsync(int userId)
{
    var followers = await _followerRepository.GetFollowersByUserIdAsync(userId);

    // Convert to DTO if needed
    return followers.Select(f => new FollowerDTO
    {
        UserId = f.UserId,
        FollowerUserId = f.FollowerUserId,
        CreatedAt = f.CreatedAt
    });
}


public async Task<bool> DeleteFollowingRelationshipAsync(int id)
        {
            return await _followerRepository.DeleteFollowingRelationshipAsync(id);
        }



         public async Task<List<UserWithMoviesDTO>> GetFollowingWithMoviesAsync(int userId)
                {
                    return await _followerRepository.GetFollowingWithMoviesAsync(userId);
                }

}