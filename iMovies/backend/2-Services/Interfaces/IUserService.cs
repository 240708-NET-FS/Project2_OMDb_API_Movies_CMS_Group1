namespace OMDbProject.Services;
using OMDbProject.Models.DTOs;
using OMDbProject.Models;
    public interface IUserService
    {
        Task<User> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO);
        Task<User> GetUserByIdAsync(int id);
    }
