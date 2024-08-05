[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto userDto)
    {
        // Implement user registration logic here
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        // Implement get user by ID logic here
    }
}
