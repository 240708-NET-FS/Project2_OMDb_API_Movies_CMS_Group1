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
       
    await Task.CompletedTask; // Placeholder for await
      return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserMovie(int id)
    {
 await Task.CompletedTask; // Placeholder for await
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserMovie(int id, UserMovieDTO userMovieDTO)
    {
   await Task.CompletedTask; // Placeholder for await
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserMovie(int id)
    {
   await Task.CompletedTask; // Placeholder for await
      return Ok();
    }
}
