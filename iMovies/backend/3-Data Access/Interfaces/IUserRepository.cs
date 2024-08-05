using OMDbProject.Models;
using System.Threading.Tasks;

namespace OMDbProject.Repositories.Interfaces;

    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUserNameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }

