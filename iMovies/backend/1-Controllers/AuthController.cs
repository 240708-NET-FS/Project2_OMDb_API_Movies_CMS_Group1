using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;
using System.Threading.Tasks;

namespace OMDbProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Authenticate and get JWT
                var token = await _authService.LoginAsync(loginDTO);

                // Return the JWT as a response
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Handle any authentication errors
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
             try
            {
                // Invalidate the current user's JWT
                await _authService.LogoutAsync();

                // Return success
                return Ok(new { Message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                // Handle any errors during logout
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
