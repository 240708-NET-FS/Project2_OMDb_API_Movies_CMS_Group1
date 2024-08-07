using System.Threading.Tasks; // For Task
using OMDbProject.Models; // For User
using OMDbProject.Models.DTOs; // For UserRegistrationDTO

namespace OMDbProject.Services.Interfaces;
    public interface IUserService
    {
        Task<UserRegistrationResponseDTO> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO);
        Task<User> GetUserByIdAsync(int id);
    }
