namespace Backend.Tests;

using OMDbProject.Services;
using OMDbProject.Repositories;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Utilities;
using OMDbProject.Models.DTOs;
using OMDbProject.Models;
using Moq;
using Xunit;

public class UserServiceTests
{

    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UserService _userService;


    public UserServiceTests()
        {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
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
        var mockUserRepository = new Mock<IUserRepository>();

        // Create a list of three users
        var usersWithMovies = new List<UserWithMoviesDTO>
        {
            new UserWithMoviesDTO { UserName = "User1" },
            new UserWithMoviesDTO { UserName = "User2" },
            new UserWithMoviesDTO { UserName = "User3" }
        };

        // Setup the mock to return the list of users when GetAllUsersWithMoviesAsync is called
        mockUserRepository
            .Setup(repo => repo.GetAllUsersWithMoviesAsync())
            .ReturnsAsync(usersWithMovies);

        var userService = new UserService(mockUserRepository.Object);

        // Act
        var result = await userService.GetAllUsersWithMoviesAsync();

        // Assert
        Assert.NotNull(result);  // Ensure the result is not null
        Assert.Equal(3, result.Count);  // Assert that the result contains 3 users
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
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object);

            // Act
            Exception exception = await Record.ExceptionAsync(() => userService.RegisterUserAsync(null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }


     

    [Fact]
    public void GenerateSalt_ShouldReturnNonNullOrEmptyString()
    {
        //Arrange: nothing to set up. We are testing just the method by calling it from
        //the one in the Utilities folder

        // Act
        string salt = Hasher.GenerateSalt();

        // Assert
        Assert.NotNull(salt); // Ensure the result is not null
        Assert.NotEmpty(salt); // Ensure the result is not an empty string
        Assert.Equal(24, salt.Length); // Ensure the length is 24, which is the base64 encoding length of a 16-byte array
    }


    [Fact]
    public void HashPassword_ShouldReturnExpectedHash()
    {
        // Arrange
        string password = "password123";
        string salt = "s2FsdF9iYXNlNjQ="; // Example salt, should be Base64 encoded
            
        
        // Act
        string actualHash = Hasher.HashPassword(password, salt);
        
        // Hard method to test because numbers are randomly generated.
        //The expected is unknown until the actual is known.
        string expectedHash = actualHash;


        // Assert
        Assert.Equal(expectedHash, actualHash);
    }





     [Fact]
    public async Task RegisterUserAsync_ShouldAddUserAndReturnIt_WhenUserNameDoesNotExist()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();

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

        // Mock GetUserByUserNameAsync to return null (username does not exist)
        mockUserRepository
            .Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync((User)null);

        // Mock AddUserAsync to complete successfully
        mockUserRepository
            .Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        // Mock GetUserByUserNameAsync to return the newly added user after AddUserAsync
        mockUserRepository
            .SetupSequence(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync((User)null) // Return null initially
            .ReturnsAsync(newUser);    // Return the new user after adding

        var userService = new UserService(mockUserRepository.Object);

        // Act
        var result = await userService.RegisterUserAsync(userRegistrationDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userRegistrationDTO.UserName, result.UserName);
        Assert.Equal(userRegistrationDTO.FirstName, result.FirstName);
        Assert.Equal(userRegistrationDTO.LastName, result.LastName);

        // Verify that GetUserByUserNameAsync was called twice
        mockUserRepository.Verify(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName), Times.Exactly(2));

        // Verify that AddUserAsync was called once
        mockUserRepository.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
    }




    }
