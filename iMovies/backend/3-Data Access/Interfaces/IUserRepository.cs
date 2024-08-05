using OMDbProject.Models;
using System.Threading.Tasks;

namespace OMDbProject.Repositories;

    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

