using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;

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
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
