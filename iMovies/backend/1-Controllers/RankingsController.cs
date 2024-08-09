using Microsoft.AspNetCore.Mvc;
using OMDbProject.Services.Interfaces;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RankingsController : ControllerBase
{
    private readonly IRankingService _rankingService;

    public RankingsController(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopRankedMovies()
    {
        var rankings = await _rankingService.GetTopRankedMoviesAsync();

        if (rankings == null || !rankings.Any())
        {
            return NotFound("No rankings found.");
        }

        return Ok(rankings);
    }
}
