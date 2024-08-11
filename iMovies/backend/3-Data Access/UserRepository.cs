using Microsoft.EntityFrameworkCore;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;

using System.Threading.Tasks;

namespace OMDbProject.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> GetUserByUserNameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<List<UserWithMoviesDTO>> GetAllUsersWithMoviesAsync()
    {
        var users = await _context.Users
            .Include(u => u.UserMovies)
            .ToListAsync();

        return users.Select(user => new UserWithMoviesDTO
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserMovies = user.UserMovies.Select(um => new UserMovieDTO
            {
                UserMovieId = um.UserMovieId,
                UserId = um.UserId,
                OMDBId = um.OMDBId,
                UserRating = um.UserRating,
                UserReview = um.UserReview,
                CreatedAt = um.CreatedAt,
                UpdatedAt = um.UpdatedAt
            }).ToList()
        }).ToList();
    }



}

