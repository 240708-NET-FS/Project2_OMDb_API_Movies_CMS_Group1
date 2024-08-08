using OMDbProject.Models;
using OMDbProject.Models.DTOs;


namespace OMDbProject.Repositories.Interfaces;

    public interface IFollowerRepository
    
    {
         Task<bool> AddFollowerAsync(Follower follower);        
         Task<IEnumerable<Follower>> GetFollowersByUserIdAsync(int userId);
         Task<bool> DeleteFollowingRelationshipAsync(int id);
         Task<List<UserWithMoviesDTO>> GetFollowingWithMoviesAsync(int userId);

    }