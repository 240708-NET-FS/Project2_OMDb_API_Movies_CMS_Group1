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

        public async Task<IEnumerable<Like>> GetLikesForUserMovieAsync(int userMovieId)
        {
                return await _context.Likes
                .Where(l => l.UserMovieId == userMovieId)
                .ToListAsync();
        
        }


        public async Task<bool> DeleteLikeAsync(int likeId)
    {
        var like = await _context.Likes.FindAsync(likeId);
        if (like == null)
        {
            return false; // Like not found
        }

        _context.Likes.Remove(like);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> LikeExistsAsync(int likeId)
    {
        return await _context.Likes.AnyAsync(l => l.LikeId == likeId);
    }
    
    }

