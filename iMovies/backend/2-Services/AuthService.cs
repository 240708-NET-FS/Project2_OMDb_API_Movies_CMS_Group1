using System.Threading.Tasks;
using OMDbProject.Models.DTOs;

namespace OMDbProject.Services
{
    public class AuthService
    {
        public Task<string> LoginAsync(LoginDTO loginDto)
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
