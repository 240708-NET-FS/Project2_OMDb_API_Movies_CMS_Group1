using OMDbProject.Models;
using System.Threading.Tasks;

namespace OMDbProject.Repositories.Interfaces
{
    public interface ILikeRepository
    {
        Task<bool> AddLikeAsync(Like like);
        Task<Like?> GetLikeByUserAndMovieAsync(int userId, int userMovieId);
        Task<IEnumerable<Like>> GetLikesForUserMovieAsync(int userMovieId);
        Task<bool> DeleteLikeAsync(int likeId);
        Task<bool> LikeExistsAsync(int likeId);
    }
}
