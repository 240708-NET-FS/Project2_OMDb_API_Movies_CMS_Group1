using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;
using OMDbProject.Repositories;
using Xunit;
using Microsoft.Extensions.Options;

using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;


public class AuthServiceTests
{
    //  private readonly AuthService _authService;
    //private readonly IUserRepository _userRepository;
    //  private readonly Mock<IUserRepository> _mockUserRepository;
    //private readonly ApplicationDbContext _context;
    private readonly DbContextOptions<ApplicationDbContext> _options;
    private readonly IOptions<JwtSettings> _jwtSettings;


    public AuthServiceTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase123" + Guid.NewGuid())
           .Options;

        //  _mockUserRepository = new Mock<IUserRepository>();
        //_userRepository = new UserRepository(_context);
        //authService = new AuthService(_userRepository, jwtSettings);
        _jwtSettings = Options.Create(new JwtSettings
        {
            Secret = "this_is_my_secret_key_this_has_to_be_long_enough_to_work",
            ExpiryInMinutes = 30
        });


    }

    private void SeedDatabase()
    {
        var context = new ApplicationDbContext(_options);
        // Clear existing data
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // Seed data
        var password = "password";
        var salt = "saltsaltsalt";
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var hmac = new HMACSHA512(saltBytes);
        var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

        var user = new User
        {
            UserId = 1,
            UserName = "testuser",
            FirstName = "Owusu",
            LastName = "Achamfour",
            PasswordHash = passwordHash,
            Salt = salt,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(user);
        context.SaveChanges();
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsUserResponseDTO()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var userRepository = new UserRepository(context);
        var authService = new AuthService(userRepository, _jwtSettings);


        var loginDTO = new LoginDTO
        {
            UserName = "testuser",
            Password = "password"
        };

        // Act
        var result = await authService.LoginAsync(loginDTO);

        // Assert
        Assert.Equal("testuser", result.UserName);
    }


    /*
        [Fact]
        public async Task LoginAsync_InvalidUsername_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            // Seed database before test
            SeedDatabase();

            // Create a new instance of the mock user repository for this test
            var mockUserRepository = new Mock<IUserRepository>();

            // Set up the mock to return null for an invalid username
            mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync("invaliduser"))
                .ReturnsAsync((User)null);

            // Create an instance of AuthService with the new mock repository
            var authService = new AuthService(mockUserRepository.Object, _jwtSettings);

            var loginDTO = new LoginDTO
            {
                UserName = "invaliduser",
                Password = "password"
            };

            // Act
            Func<Task> act = async () => await authService.LoginAsync(loginDTO);

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(act);
        }
    */
    /*
        [Fact]
        public async Task LoginAsync_InvalidPassword_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "testuser",
                PasswordHash = "passwordHash",
                Salt = "salt"
            };

            var loginDTO = new LoginDTO
            {
                UserName = "testuser",
                Password = "wrongpassword"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(loginDTO.UserName))
                .ReturnsAsync(user);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(loginDTO));
        }




    [Fact]
    public async Task LoginAsync_ValidCredentials_GeneratesJwtToken()
    {
        // Arrange
        var user = new User
        {
            UserId = 1,
            UserName = "testuser",
            PasswordHash = "passwordHash",
            Salt = "salt"
        };

        var loginDTO = new LoginDTO
        {
            UserName = "testuser",
            Password = "password"
        };

        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(loginDTO.UserName))
            .ReturnsAsync(user);

        // Act
        var result = await _authService.LoginAsync(loginDTO);

        // Assert
        Assert.NotNull(result.Token);
    }


    [Fact]
    public async Task LogoutAsync_CompletesSuccessfully()
    {
        // Act
        await _authService.LogoutAsync();

        // Assert
        Assert.True(true); // No exception thrown
    }





    [Fact]
    public async Task LoginAsync_InvalidPassword_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var correctPassword = "password";
        var incorrectPassword = "wrongpassword";
        var salt = "saltsaltsalt";
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var hmac = new HMACSHA512(saltBytes);
        var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(correctPassword)));

        var user = new User
        {
            UserId = 1,
            UserName = "testuser",
            PasswordHash = passwordHash,
            Salt = salt
        };

        var loginDTO = new LoginDTO
        {
            UserName = "testuser",
            Password = incorrectPassword
        };

        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(loginDTO.UserName))
            .ReturnsAsync(user);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
        {
            await _authService.LoginAsync(loginDTO);
        });

        // Assert
        Assert.Equal("Invalid username or password.", exception.Message);
    }
*/







}




