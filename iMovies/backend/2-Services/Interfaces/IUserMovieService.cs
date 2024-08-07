using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Services.Interfaces;

    public interface IUserMovieService
    {
       Task<UserMovie> AddUserMovieAsync(UserMovieDTO userMovieDTO);
       Task<UserMovie> GetUserMovieByIdAsync(int id);
       Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync();
       Task<UserMovie> UpdateUserMovieAsync(int id, UserMovieDTO userMovieDTO);
       Task<bool> DeleteUserMovieAsync(int id);
       Task<IEnumerable<UserMovieDTO>> GetMoviesByUserIdAsync(int userId);
    }

