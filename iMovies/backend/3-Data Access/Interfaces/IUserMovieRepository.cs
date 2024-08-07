using OMDbProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Repositories.Interfaces;

    public interface IUserMovieRepository
    {
          Task<UserMovie> AddUserMovieAsync(UserMovie userMovie);
//        Task<IEnumerable<UserMovie>> GetUserMoviesAsync();
 //       Task<UserMovie> GetUserMovieByIdAsync(int id);
  //      Task<UserMovie> UpdateUserMovieAsync(UserMovie userMovie);
   //     Task<bool> DeleteUserMovieAsync(int id);
   }

