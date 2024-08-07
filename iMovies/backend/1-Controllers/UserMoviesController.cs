using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Services.Interfaces;
using OMDbProject.Models.DTOs;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserMoviesController : ControllerBase
{
    private readonly IUserMovieService _userMovieService;

    public UserMoviesController(IUserMovieService userMovieService)
    {
        _userMovieService = userMovieService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserMovie([FromBody] UserMovieDTO userMovieDTO)
    {
        var userMovie = await _userMovieService.AddUserMovieAsync(userMovieDTO);
        
        var responseMovieDTO = new UserMovieDTO {
            UserMovieId = userMovie.UserMovieId,
            UserId = userMovie.UserId,
            OMDBId = userMovie.OMDBId,
            UserReview = userMovie.UserReview,
            UserRating = userMovie.UserRating
        };

        return CreatedAtAction(nameof(GetUserMovie), new { id = userMovie.UserMovieId }, responseMovieDTO);
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
