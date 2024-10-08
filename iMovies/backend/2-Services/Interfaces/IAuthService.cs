using System.Threading.Tasks;
using OMDbProject.Models.DTOs;

namespace OMDbProject.Services.Interfaces;

    public interface IAuthService
    {
        Task<UserResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
      
    }

