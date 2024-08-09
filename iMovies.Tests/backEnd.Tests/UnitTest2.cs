using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using OMDbProject.Models;
using OMDbProject.Repositories;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;
using OMDbProject.Models.DTOs;
 
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UserService _userService;
 
    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }
    
    /*[Fact]
    public async Task RegisterUserAsync_ShouldReturnUserRegistrationResponseDTO_AfterUserIsAdded()
    {
        // Arrange
        var userRegistrationDTO = new UserRegistrationDTO
        {
            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Password = "securepassword"
        };
    
        var addedUser = new User
        {
            UserId = 0,
            FirstName = "Jane",
            LastName = "Doe",
            UserName = "johnsmith",
            PasswordHash = "hashedpassword",
            Salt = "somesalt",
            CreatedAt = DateTime.UtcNow
        };

        // Setup the repository to return null when searching for the username (indicating the user doesn't exist)
        _mockUserRepository
            .Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync((User)null);
    
        // Setup the repository to simulate adding the user
        _mockUserRepository
            .Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
            
        // Setup the repository to return the added user
        _mockUserRepository
            .Setup(repo => repo.GetUserByUserNameAsync(userRegistrationDTO.UserName))
            .ReturnsAsync(addedUser);
    
        // Act
        var result = await _userService.RegisterUserAsync(userRegistrationDTO);
    
        // Assert
        Assert.IsType<UserRegistrationResponseDTO>(result);
    }*/
    
    [Fact]
    public async Task GetAllUsersWithMoviesAsync_ShouldReturnListOfUserWithMoviesDTO()
    {
        // Arrange
        var userWithMovies = new List<UserWithMoviesDTO>
        {
            new UserWithMoviesDTO
            {
                UserId = 0,
                UserName = "john_doe",
                Movies = new List<MovieDTO>
                {
                    new MovieDTO { MovieId = 0, Title = "Movie 1" },
                    new MovieDTO { MovieId = 1, Title = "Movie 2" }
                }
            },
            new UserWithMoviesDTO
            {
                UserId = 0,
                UserName = "jane_doe",
                Movies = new List<MovieDTO>
                {
                    new MovieDTO { MovieId = 0, Title = "Movie 3" }
                }
            }
        };
    
        // Mock the repository to return the list of UserWithMoviesDTO
        _mockUserRepository
            .Setup(repo => repo.GetAllUsersWithMoviesAsync())
            .ReturnsAsync(userWithMovies);
    
        // Act
        var result = await _userService.GetAllUsersWithMoviesAsync();
    
        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<UserWithMoviesDTO>>(result);
        Assert.Equal(userWithMovies.Count, result.Count);
    
        for (int i = 0; i < userWithMovies.Count; i++)
        {
            Assert.Equal(userWithMovies[i].UserId, result[i].UserId);
            Assert.Equal(userWithMovies[i].UserName, result[i].UserName);
            Assert.Equal(userWithMovies[i].Movies.Count, result[i].Movies.Count);
    
            for (int j = 0; j < userWithMovies[i].Movies.Count; j++)
            {
                Assert.Equal(userWithMovies[i].Movies[j].MovieId, result[i].Movies[j].MovieId);
                Assert.Equal(userWithMovies[i].Movies[j].Title, result[i].Movies[j].Title);
            }
        }
    
        // Verify that GetAllUsersWithMoviesAsync was called once
        _mockUserRepository.Verify(repo => repo.GetAllUsersWithMoviesAsync(), Times.Once);
    }
}