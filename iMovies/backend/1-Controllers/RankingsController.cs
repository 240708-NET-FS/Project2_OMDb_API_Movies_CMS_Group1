using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models.DTOs;
using OMDBProject.Services;

[ApiController]
[Route("api/[controller]")]
public class RankingsController : ControllerBase
{
    private readonly RankingsService _rankingsService;

    public RankingsController(RankingsService rankingsService)
    {
        _rankingsService = rankingsService;
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopRankedMovies()
    {
        
    }
}
