namespace Backend.Tests;

using OMDbProject.Services;
using OMDbProject.Repositories;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Utilities;
using OMDbProject.Models.DTOs;
using OMDbProject.Models;
using OMDbProject.Utilities.Interfaces;
using System.Security.Cryptography; // For HMACSHA512
using System.Text; // For Encoding

using Moq;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IHasher> _mockHasher;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockHasher = new Mock<IHasher>();
        _userService = new UserService(_mockUserRepository.Object, _mockHasher.Object);
    }

    [Fact]
    public async Task GetAllUsersWithMoviesAsync_ShouldReturnListOfUsersWithMovies()
    {
        // Arrange
        var usersWithMovies = new List<UserWithMoviesDTO>
        {
            new UserWithMoviesDTO { UserName = "User1" }
        };

        _mockUserRepository.Setup(repo => repo.GetAllUsersWithMoviesAsync()).ReturnsAsync(usersWithMovies);

        // Act
        var result = await _userService.GetAllUsersWithMoviesAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllUsersWithMoviesAsync_ShouldReturnThreeUsers()
    {
        // Arrange
        var usersWithMovies = new List<UserWithMoviesDTO>
        {
            new UserWithMoviesDTO { UserName = "User1" },
            new UserWithMoviesDTO { UserName = "User2" },
            new UserWithMoviesDTO { UserName = "User3" }
        };

        _mockUserRepository.Setup(repo => repo.GetAllUsersWithMoviesAsync()).ReturnsAsync(usersWithMovies);

        var userService = new UserService(_mockUserRepository.Object, _mockHasher.Object);

        // Act
        var result = await userService.GetAllUsersWithMoviesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser()
    {
        // Arrange
        var user = new User { UserId = 1, UserName = "TestUser" };

        _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(user.UserId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(user.UserId);

        // Assert
        Assert.Equal(user.UserId, result.UserId);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldThrowArgumentNullException_WhenUserRegistrationDTOIsNull()
    {
        // Arrange
        var userService = new UserService(_mockUserRepository.Object, _mockHasher.Object);

        // Act
        Exception exception = await Record.ExceptionAsync(() => userService.RegisterUserAsync(null));

        // Assert
        Assert.IsType<ArgumentNullException>(exception);
    }


    [Fact]
    public void GenerateSalt_ShouldReturnNonNullOrEmptyString()
    {
        // Arrange
        // This base64 string represents 16 bytes
        string base64StringFor16Bytes = Convert.ToBase64String(new byte[16]);
        _mockHasher.Setup(hasher => hasher.GenerateSalt()).Returns(base64StringFor16Bytes);

        // Act
        string salt = _mockHasher.Object.GenerateSalt();

        // Assert
        Assert.NotNull(salt);
        Assert.NotEmpty(salt);
        Assert.Equal(16, Convert.FromBase64String(salt).Length);
    }


    [Fact]
    public void HashPassword_ShouldReturnExpectedHash()
    {
        // Arrange
        string password = "password123";
        string salt = "s2FsdF9iYXNlNjQ=";
        string expectedHash = Convert.ToBase64String(new HMACSHA512(Convert.FromBase64String(salt)).ComputeHash(Encoding.UTF8.GetBytes(password)));

        _mockHasher.Setup(hasher => hasher.HashPassword(password, salt)).Returns(expectedHash);

        // Act
        string actualHash = _mockHasher.Object.HashPassword(password, salt);

        // Assert
        Assert.Equal(expectedHash, actualHash);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldAddUserAndReturnIt_WhenUserNameDoesNotExist()
    {
        // Arrange
        var userRegistrationDTO = new UserRegistrationDTO
        {
            UserName = "newUser",
            Password = "password",
            FirstName = "John",
            LastName = "Doe"
        };

        var newUser = new User
        {
            UserId = 1,
            UserName = userRegistrationDTO.UserName,
            FirstName = userRegistrationDTO.FirstName,
            LastName = userRegistrationDTO.LastName,
            PasswordHash = "hashedPassword",
            Salt = "salt",
            CreatedAt = DateTime.UtcNow
        };

        _mockHasher.Setup(hasher => hasher.GenerateSalt()).Returns("salt");
        _mockHasher.Setup(hasher => hasher.HashPassword(userRegistrationDTO.Password, "salt")).Returns("hashedPassword");

        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName)).ReturnsAsync((User)null);
        _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _mockUserRepository.SetupSequence(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync((User)null)
            .ReturnsAsync(newUser);

        var userService = new UserService(_mockUserRepository.Object, _mockHasher.Object);

        // Act
        var result = await userService.RegisterUserAsync(userRegistrationDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userRegistrationDTO.UserName, result.UserName);
        Assert.Equal(userRegistrationDTO.FirstName, result.FirstName);
        Assert.Equal(userRegistrationDTO.LastName, result.LastName);

        _mockUserRepository.Verify(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName), Times.Exactly(2));
        _mockUserRepository.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
    }


    [Fact]
    public async Task RegisterUserAsync_ShouldThrowException_WhenUserNameIsAlreadyTaken()
    {
        // Arrange
        var userRegistrationDTO = new UserRegistrationDTO
        {
            UserName = "existingUser",
            Password = "password",
            FirstName = "John",
            LastName = "Doe"
        };

        // Mock the repository to return a non-null user (username already taken)
        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync(new User { UserName = userRegistrationDTO.UserName });

        // Act
        Exception exception = await Record.ExceptionAsync(() => _userService.RegisterUserAsync(userRegistrationDTO));

        // Assert
        Assert.IsType<Exception>(exception);
        Assert.Equal("Username is already taken.", exception.Message);
    }



    [Fact]
    public async Task RegisterUserAsync_ShouldThrowException_WhenUserIsNotAddedCorrectly()
    {
        // Arrange
        var userRegistrationDTO = new UserRegistrationDTO
        {
            UserName = "newUser",
            Password = "password",
            FirstName = "John",
            LastName = "Doe"
        };

        // Mock the repository methods
        _mockHasher.Setup(hasher => hasher.GenerateSalt()).Returns("salt");
        _mockHasher.Setup(hasher => hasher.HashPassword(userRegistrationDTO.Password, "salt")).Returns("hashedPassword");
        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName)).ReturnsAsync((User)null);
        _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync((User)null); // Simulating user is not added

        // Act
        Exception exception = await Record.ExceptionAsync(() => _userService.RegisterUserAsync(userRegistrationDTO));

        // Assert
        Assert.IsType<Exception>(exception);
        Assert.Equal("User was not added correctly.", exception.Message);
    }





}
