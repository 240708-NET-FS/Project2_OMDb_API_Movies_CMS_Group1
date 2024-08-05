using Microsoft.AspNetCore.Mvc;
using OMDbProject.Services.Interfaces;
using OMDbProject.Models.DTOs;
using OMDbProject.Services;

[ApiController]
[Route("api/[controller]")]
public class RankingsController : ControllerBase
{
    private readonly RankingService _rankingService;

    public RankingsController(RankingService rankingService)
    {
        _rankingService = rankingService;
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopRankedMovies()
    {
          return Ok();
    await Task.CompletedTask; // Placeholder for await
    }
}
