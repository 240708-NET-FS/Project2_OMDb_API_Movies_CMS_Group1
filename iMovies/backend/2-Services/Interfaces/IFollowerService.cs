using OMDbProject.Models.DTOs;

namespace OMDbProject.Services.Interfaces;

public interface IFollowerService
{
    Task<bool> AddFollowerAsync(FollowerDTO followerDTO);

     Task<IEnumerable<FollowerDTO>> GetFollowersByUserIdAsync(int userId);
}