using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;

namespace OMDbProject.Services.Interfaces;

public interface IFollowerService
{
    Task<Follower> AddFollowerAsync(FollowerDTO followerDTO);

     Task<IEnumerable<FollowerDTO>> GetFollowersByUserIdAsync(int userId);

     Task<bool> DeleteFollowingRelationshipAsync(int id);
     Task<List<UserWithMoviesDTO>> GetFollowingWithMoviesAsync(int userId);
}