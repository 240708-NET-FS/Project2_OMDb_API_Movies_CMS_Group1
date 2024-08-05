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
    public async Task<IActionResult> AddUserMovie(UserMovieDto userMovieDto)
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
    public async Task<IActionResult> UpdateUserMovie(int id, UserMovieDto userMovieDto)
    {
      
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserMovie(int id)
    {
      
    }
}
