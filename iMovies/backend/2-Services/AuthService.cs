using System.Threading.Tasks;
using OMDbProject.Models.DTOs;
using OMDbProject.Services.Interfaces;

namespace OMDbProject.Services
{
    public class AuthService : IAuthService
    {
        public Task<string> LoginAsync(LoginDTO loginDTO)
        {
            // Validate the user credentials and return a JWT
            return Task.FromResult(string.Empty);
        }

        public Task LogoutAsync()
        {
            
            //Invalidate the current user's JWT
            return Task.CompletedTask;
        }
    }
}
