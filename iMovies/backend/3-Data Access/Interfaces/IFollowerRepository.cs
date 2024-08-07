using OMDbProject.Models;


namespace OMDbProject.Repositories.Interfaces;

    public interface IFollowerRepository
    
    {
         Task<bool> AddFollowerAsync(Follower follower);        
         Task<IEnumerable<Follower>> GetFollowersByUserIdAsync(int userId);

    }