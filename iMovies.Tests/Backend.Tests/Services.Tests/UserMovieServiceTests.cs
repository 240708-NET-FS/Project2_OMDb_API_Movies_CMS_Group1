using Moq;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMDbProject.Tests
{
    public class UserMovieServiceTests
    {
        private readonly UserMovieService _userMovieService;
        private readonly Mock<IUserMovieRepository> _mockUserMovieRepository;

        public UserMovieServiceTests()
        {
            _mockUserMovieRepository = new Mock<IUserMovieRepository>();
            _userMovieService = new UserMovieService(_mockUserMovieRepository.Object);
        }

        // Existing tests ...

        [Fact]
        public async Task UpdateUserMovieAsync_ShouldReturnNullWhenUserMovieNotFound()
        {
            // Arrange
            var id = 1;
            var userMovieDTO = new UserMovieDTO
            {
                UserRating = 4,
                UserReview = "Updated review"
            };

            _mockUserMovieRepository
                .Setup(repo => repo.GetUserMovieByIdAsync(id))
                .ReturnsAsync((UserMovie)null);

            // Act
            var result = await _userMovieService.UpdateUserMovieAsync(id, userMovieDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUserMovieAsync_ShouldReturnFalseWhenDeletionFails()
        {
            // Arrange
            var id = 1;

            _mockUserMovieRepository
                .Setup(repo => repo.DeleteUserMovieAsync(id))
                .ReturnsAsync(false);

            // Act
            var result = await _userMovieService.DeleteUserMovieAsync(id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddUserMovieAsync_ShouldHandleNullUserMovieDTO()
        {
            // Arrange
            UserMovieDTO userMovieDTO = null;

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _userMovieService.AddUserMovieAsync(userMovieDTO));
        }

        [Fact]
        public async Task UpdateUserMovieAsync_ShouldHandleNullUserMovieDTO()
        {
            // Arrange
            var id = 1;
            UserMovieDTO userMovieDTO = null;

            var existingUserMovie = new UserMovie
            {
                UserMovieId = id,
                UserId = 1,
                OMDBId = "tt1234567",
                UserRating = 3,
                UserReview = "Old review"
            };

            _mockUserMovieRepository
                .Setup(repo => repo.GetUserMovieByIdAsync(id))
                .ReturnsAsync(existingUserMovie);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _userMovieService.UpdateUserMovieAsync(id, userMovieDTO));
        }

        [Fact]
        public async Task GetUserMovieByIdAsync_ShouldReturnNullWhenUserMovieNotFound()
        {
            // Arrange
            var id = 1;

            _mockUserMovieRepository
                .Setup(repo => repo.GetUserMovieByIdAsync(id))
                .ReturnsAsync((UserMovie)null);

            // Act
            var result = await _userMovieService.GetUserMovieByIdAsync(id);

            // Assert
            Assert.Null(result);
        }
    }
}
