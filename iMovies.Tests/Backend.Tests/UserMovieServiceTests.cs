using Moq;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;

namespace OMDbProject.Tests;

    public class UserMovieServiceTests
    {
        private readonly UserMovieService _userMovieService;
        private readonly Mock<IUserMovieRepository> _mockUserMovieRepository;

        public UserMovieServiceTests()
        {
            // Initialize the mock repository
            _mockUserMovieRepository = new Mock<IUserMovieRepository>();

            // Initialize the UserMovieService with the mock repository
            _userMovieService = new UserMovieService(_mockUserMovieRepository.Object);
        }

        

        [Fact]
        public async Task AddUserMovieAsync_ShouldCallRepositoryAddMethod()
        {
        // Arrange
        var userMovieDTO = new UserMovieDTO
        {
            UserId = 1,
            OMDBId = "tt1234567",
            UserRating = 5,
            UserReview = "Great movie!"
        };

        _mockUserMovieRepository
            .Setup(repo => repo.AddUserMovieAsync(It.IsAny<UserMovie>()))
            .ReturnsAsync(new UserMovie { UserMovieId = 1 });

        // Act
        var result = await _userMovieService.AddUserMovieAsync(userMovieDTO);

        // Assert
        _mockUserMovieRepository.Verify(repo => repo.AddUserMovieAsync(It.Is<UserMovie>(um =>
            um.UserId == userMovieDTO.UserId &&
            um.OMDBId == userMovieDTO.OMDBId &&
            um.UserRating == userMovieDTO.UserRating &&
            um.UserReview == userMovieDTO.UserReview
        )), Times.Once);
        
    }


    [Fact]
public async Task UpdateUserMovieAsync_ShouldCallRepositoryUpdateMethod()
{
    // Arrange
    var id = 1;
    var userMovieDTO = new UserMovieDTO
    {
        UserRating = 4,
        UserReview = "Updated review"
    };

    var existingUserMovie = new UserMovie
    {
        UserMovieId = id,
        UserRating = 3,
        UserReview = "Old review"
    };

    _mockUserMovieRepository
        .Setup(repo => repo.GetUserMovieByIdAsync(id))
        .ReturnsAsync(existingUserMovie);

    _mockUserMovieRepository
        .Setup(repo => repo.UpdateUserMovieAsync(It.IsAny<UserMovie>()))
        .ReturnsAsync(new UserMovie { UserMovieId = id, UserRating = 4, UserReview = "Updated review" });

    // Act
    var result = await _userMovieService.UpdateUserMovieAsync(id, userMovieDTO);

    // Assert
    _mockUserMovieRepository.Verify(repo => repo.UpdateUserMovieAsync(It.Is<UserMovie>(um =>
        um.UserMovieId == id &&
        um.UserRating == userMovieDTO.UserRating &&
        um.UserReview == userMovieDTO.UserReview
    )), Times.Once);
}


[Fact]
public async Task GetUserMovieByIdAsync_ShouldReturnUserMovie()
{
    // Arrange
    var id = 1;
    var userMovie = new UserMovie
    {
        UserMovieId = id,
        UserRating = 5,
        UserReview = "Great movie!"
    };

    _mockUserMovieRepository
        .Setup(repo => repo.GetUserMovieByIdAsync(id))
        .ReturnsAsync(userMovie);

    // Act
    var result = await _userMovieService.GetUserMovieByIdAsync(id);

    // Assert
    Assert.NotNull(result);
}


[Fact]
public async Task GetAllUserMoviesAsync_ShouldReturnAllUserMovies()
{
    // Arrange
    var userMovies = new List<UserMovie>
    {
        new UserMovie { UserMovieId = 1 },
        new UserMovie { UserMovieId = 2 }
    };

    _mockUserMovieRepository
        .Setup(repo => repo.GetAllUserMoviesAsync())
        .ReturnsAsync(userMovies);

    // Act
    var result = await _userMovieService.GetAllUserMoviesAsync();

    // Assert
    Assert.Equal(2, result.Count());
}


[Fact]
public async Task DeleteUserMovieAsync_ShouldReturnTrueWhenDeleted()
{
    // Arrange
    var id = 1;

    _mockUserMovieRepository
        .Setup(repo => repo.DeleteUserMovieAsync(id))
        .ReturnsAsync(true);

    // Act
    var result = await _userMovieService.DeleteUserMovieAsync(id);

    // Assert
    Assert.True(result);
}


[Fact]
public async Task GetMoviesByUserIdAsync_ShouldReturnUserMovieDTOs()
{
    // Arrange
    var userId = 1;
    var userMovies = new List<UserMovie>
    {
        new UserMovie { UserId = userId, OMDBId = "tt1234567" },
        new UserMovie { UserId = userId, OMDBId = "tt7654321" }
    };

    _mockUserMovieRepository
        .Setup(repo => repo.GetUserMoviesByUserIdAsync(userId))
        .ReturnsAsync(userMovies);

    // Act
    var result = await _userMovieService.GetMoviesByUserIdAsync(userId);

    // Assert
    Assert.NotEmpty(result);
}


    }

