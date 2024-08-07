using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services.Interfaces;
using System.Threading.Tasks;

namespace OMDbProject.Services;

    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<bool> AddLikeAsync(LikeDTO likeDTO)
        {

             // Check if the user is already liking this movie
            var existingLike = await _likeRepository.GetLikeByUserAndMovieAsync(likeDTO.UserId, likeDTO.UserMovieId);

            if (existingLike != null)
            {
                // Like already exists
                return false;
            }

            var like = new Like
            {
                UserId = likeDTO.UserId,
                UserMovieId = likeDTO.UserMovieId,
                CreatedAt = DateTime.UtcNow
            };

            return await _likeRepository.AddLikeAsync(like);
        }
    

            public async Task<IEnumerable<LikeDTO>> GetLikesForUserMovieAsync(int userMovieId)
        {
                var likes = await _likeRepository.GetLikesForUserMovieAsync(userMovieId);

                    // Convert to DTO if needed
                return likes.Select(l => new LikeDTO
                 {
                    UserId = l.UserId,
                    UserMovieId = l.UserMovieId,
                    CreatedAt = l.CreatedAt
                 });
}




}
