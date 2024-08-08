using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Services;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Services;

    public class UserMovieService : IUserMovieService
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public UserMovieService(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<UserMovie> AddUserMovieAsync(UserMovieDTO userMovieDTO)
        {
            var userMovie = new UserMovie
            {
                UserId = userMovieDTO.UserId,
                OMDBId = userMovieDTO.OMDBId,
                UserRating = userMovieDTO.UserRating,
                UserReview = userMovieDTO.UserReview,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _userMovieRepository.AddUserMovieAsync(userMovie);
        }


 public async Task<UserMovie> UpdateUserMovieAsync(int id, UserMovieDTO userMovieDTO)
        {
            var userMovie = await _userMovieRepository.GetUserMovieByIdAsync(id);
            if (userMovie == null)
            {
                return null;
            }

            userMovie.UserRating = userMovieDTO.UserRating;
            userMovie.UserReview = userMovieDTO.UserReview;
            userMovie.UpdatedAt = DateTime.UtcNow;

            return await _userMovieRepository.UpdateUserMovieAsync(userMovie);
        }


         public async Task<UserMovie> GetUserMovieByIdAsync(int id)
        {
            return await _userMovieRepository.GetUserMovieByIdAsync(id);
        }


         public async Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync()
        {
            return await _userMovieRepository.GetAllUserMoviesAsync();
        }


         public async Task<bool> DeleteUserMovieAsync(int id)
        {
            return await _userMovieRepository.DeleteUserMovieAsync(id);
        }



        public async Task<IEnumerable<UserMovieDTO>> GetMoviesByUserIdAsync(int userId)
    {
        var userMovies = await _userMovieRepository.GetUserMoviesByUserIdAsync(userId);
        
        // Convert UserMovie entities to MovieDTOs
        return userMovies.Select(um => new UserMovieDTO
        {
            OMDBId = um.OMDBId,
            UserId = um.UserId,
            UserMovieId = um.UserMovieId,
            UserRating = um.UserRating,
            UserReview = um.UserReview,
            CreatedAt = um.CreatedAt,
            UpdatedAt = um.UpdatedAt

        });
        
    }


    }
