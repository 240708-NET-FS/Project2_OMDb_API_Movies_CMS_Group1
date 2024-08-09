using Microsoft.EntityFrameworkCore;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
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



         public async Task<bool> DeleteFollowingRelationshipAsync(int id)
        {
            var follower = await _context.Followers.FindAsync(id);
            if (follower != null)
            {
                _context.Followers.Remove(follower);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }



        public async Task<List<UserWithMoviesDTO>> GetFollowingWithMoviesAsync(int userId)
            {
                var followingWithMovies = await _context.Followers
                .Where(f => f.FollowerUserId == userId)
                .Select(f => new UserWithMoviesDTO
                    {
                        UserId = f.UserId,
                        FirstName = f.User.FirstName,
                        LastName = f.User.LastName,
                        UserName = f.User.UserName,
                        FollowingRelationshipId = f.FollowingRelationshipId,
                        UserMovies = f.User.UserMovies.Select(um => new UserMovieDTO
                        {
                            UserMovieId = um.UserMovieId,
                            UserId = um.UserId,
                            OMDBId = um.OMDBId,
                            UserRating = um.UserRating,
                            UserReview = um.UserReview,
                            CreatedAt = um.CreatedAt,
                            UpdatedAt = um.UpdatedAt
                        }).ToList()
                        })
                        .ToListAsync();

                        return followingWithMovies;
            }

    }

