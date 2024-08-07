using Microsoft.EntityFrameworkCore;
using OMDbProject.Models;
using OMDbProject.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OMDbProject.Repositories;

    public class FollowerRepository : IFollowerRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFollowerAsync(Follower follower)
        {

            // Check if the follower relationship already exists
            var existingFollower = await _context.Followers
                .FirstOrDefaultAsync(f => f.UserId == follower.UserId && f.FollowerUserId == follower.FollowerUserId);

            if (existingFollower != null)
            {
                // Follower relationship already exists
                return false;
            }

            _context.Followers.Add(follower);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<Follower>> GetFollowersByUserIdAsync(int userId)
        {
                return await _context.Followers
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

    }

