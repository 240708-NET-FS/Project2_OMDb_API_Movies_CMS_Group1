using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models.DTOs;
using OMDBProject.Services;

[ApiController]
[Route("api/[controller]")]
public class UserMoviesController : ControllerBase
{
    private readonly UserMoviesService _userMoviesService;

    public UserMoviesController(UserMoviesService userMoviesService)
    {
        _userMoviesService = userMoviesService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserMovie(UserMovieDTO userMovieDto)
    {
       
    }

    [HttpGet]
    public async Task<IActionResult> GetUserMovies()
    {
       
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserMovie(int id)
    {
       
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserMovie(int id, UserMovieDTO userMovieDto)
    {
      
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserMovie(int id)
    {
      
    }
}
