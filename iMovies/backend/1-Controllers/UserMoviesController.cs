using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class UserMoviesController : ControllerBase
{
    private readonly UserMovieService _userMovieService;

    public UserMoviesController(UserMovieService userMovieService)
    {
        _userMovieService = userMovieService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserMovie(UserMovieDTO userMovieDTO)
    {
       return Ok();
    await Task.CompletedTask; // Placeholder for await  
    }

    [HttpGet]
    public async Task<IActionResult> GetUserMovies()
    {
         return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserMovie(int id)
    {
         return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserMovie(int id, UserMovieDTO userMovieDTO)
    {
        return Ok();
    await Task.CompletedTask; // Placeholder for await
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserMovie(int id)
    {
        return Ok();
    await Task.CompletedTask; // Placeholder for await
    }
}
