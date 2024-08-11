using OMDbProject.Models.DTOs;
using System.Threading.Tasks;


namespace OMDbProject.Services.Interfaces;

    public interface ILikeService
    {
        Task<bool> AddLikeAsync(LikeDTORequest likeDTO);
        Task<IEnumerable<LikeDTO>> GetLikesForUserMovieAsync(int userMovieId);
        Task<bool> DeleteLikeAsync(int likeId);
    }
