using System.Threading.Tasks;
using OMDbProject.Models.DTOs;

namespace OMDbProject.Services.Interfaces;

    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }

