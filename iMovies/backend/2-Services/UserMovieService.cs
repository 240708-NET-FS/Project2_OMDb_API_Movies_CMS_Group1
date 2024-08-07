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
    }
