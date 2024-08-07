using OMDbProject.Models;
using OMDbProject.Repositories;
using OMDbProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OMDbProject.Repositories;

    public class UserMovieRepository : IUserMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserMovie> AddUserMovieAsync(UserMovie userMovie)
        {
            _context.UserMovies.Add(userMovie);
            await _context.SaveChangesAsync();
            return userMovie;
        }


         public async Task<UserMovie> UpdateUserMovieAsync(UserMovie userMovie)
        {
            _context.UserMovies.Update(userMovie);
            await _context.SaveChangesAsync();
            return userMovie;
        }


         public async Task<UserMovie> GetUserMovieByIdAsync(int id)
        {
            return await _context.UserMovies.FindAsync(id);
        }

        public async Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync()
        {
            return await _context.UserMovies
             //   .Include(um => um.User) // Include related User entities
             //   .Include(um => um.Likes) // Include related Like entities
                .ToListAsync();
        }

        public async Task<bool> DeleteUserMovieAsync(int id)
        {
            var userMovie = await _context.UserMovies.FindAsync(id);
            if (userMovie == null)
            {
                return false;
            }

            _context.UserMovies.Remove(userMovie);
            await _context.SaveChangesAsync();
            return true;
        }

    }