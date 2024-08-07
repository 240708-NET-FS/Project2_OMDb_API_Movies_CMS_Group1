using OMDbProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Repositories.Interfaces;

    public interface IUserMovieRepository
    {
          Task<UserMovie> AddUserMovieAsync(UserMovie userMovie);
          Task<UserMovie> GetUserMovieByIdAsync(int id);
          Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync();
          Task<UserMovie> UpdateUserMovieAsync(UserMovie userMovie);
         Task<bool> DeleteUserMovieAsync(int id);
   }

