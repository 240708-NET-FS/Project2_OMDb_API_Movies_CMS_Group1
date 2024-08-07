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
    }