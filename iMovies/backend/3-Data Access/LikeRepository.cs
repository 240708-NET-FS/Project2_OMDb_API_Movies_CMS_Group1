using OMDbProject.Models;
using OMDbProject.Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OMDbProject.Repositories;

    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddLikeAsync(Like like)
        {
            _context.Likes.Add(like);
            return await _context.SaveChangesAsync() > 0;
        }
    
        //GetLikeByUserAndMovieAsync method is used to check to see if a like already exists
        public async Task<Like?> GetLikeByUserAndMovieAsync(int userId, int userMovieId)
        {
                return await _context.Likes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.UserMovieId == userMovieId);
        }

    
    
    }

