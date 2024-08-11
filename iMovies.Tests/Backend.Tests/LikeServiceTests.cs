using Moq;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMDbProject.Tests;

    public class LikeServiceTests
    {
        private readonly LikeService _likeService;
        private readonly Mock<ILikeRepository> _mockLikeRepository;

        public LikeServiceTests()
        {
            _mockLikeRepository = new Mock<ILikeRepository>();
            _likeService = new LikeService(_mockLikeRepository.Object);
        }

        [Fact]
        public async Task AddLikeAsync_ShouldReturnFalse_WhenLikeAlreadyExists()
        {
            // Arrange
            var likeDTO = new LikeDTORequest { UserId = 1, UserMovieId = 1 };
            _mockLikeRepository
                .Setup(repo => repo.GetLikeByUserAndMovieAsync(likeDTO.UserId, likeDTO.UserMovieId))
                .ReturnsAsync(new Like()); // Simulate existing like

            // Act
            var result = await _likeService.AddLikeAsync(likeDTO);

            // Assert
            Assert.False(result);  // Like already exists, should return false
        }

        [Fact]
        public async Task AddLikeAsync_ShouldReturnTrue_WhenLikeDoesNotExist()
        {
            // Arrange
            var likeDTO = new LikeDTORequest { UserId = 1, UserMovieId = 1 };
            _mockLikeRepository
                .Setup(repo => repo.GetLikeByUserAndMovieAsync(likeDTO.UserId, likeDTO.UserMovieId))
                .ReturnsAsync((Like)null); // Simulate no existing like

            _mockLikeRepository
                .Setup(repo => repo.AddLikeAsync(It.IsAny<Like>()))
                .ReturnsAsync(true); // Simulate successful addition

            // Act
            var result = await _likeService.AddLikeAsync(likeDTO);

            // Assert
            Assert.True(result);  // Like does not exist, should return true
        }

        [Fact]
        public async Task GetLikesForUserMovieAsync_ShouldReturnLikesList()
        {
            // Arrange
            var userMovieId = 1;
            var likes = new List<Like>
            {
                new Like { LikeId = 1, UserId = 1, UserMovieId = userMovieId, CreatedAt = DateTime.UtcNow },
                new Like { LikeId = 2, UserId = 2, UserMovieId = userMovieId, CreatedAt = DateTime.UtcNow }
            };

            _mockLikeRepository
                .Setup(repo => repo.GetLikesForUserMovieAsync(userMovieId))
                .ReturnsAsync(likes);

            // Act
            var result = await _likeService.GetLikesForUserMovieAsync(userMovieId);

            // Assert
            Assert.Equal(2, result.Count());  // Ensure the result contains 2 likes
        }

        [Fact]
        public async Task DeleteLikeAsync_ShouldReturnFalse_WhenLikeDoesNotExist()
        {
            // Arrange
            var likeId = 1;
            _mockLikeRepository
                .Setup(repo => repo.LikeExistsAsync(likeId))
                .ReturnsAsync(false); // Simulate like does not exist

            // Act
            var result = await _likeService.DeleteLikeAsync(likeId);

            // Assert
            Assert.False(result);  // Like does not exist, should return false
        }

        [Fact]
        public async Task DeleteLikeAsync_ShouldReturnTrue_WhenLikeExists()
        {
            // Arrange
            var likeId = 1;
            _mockLikeRepository
                .Setup(repo => repo.LikeExistsAsync(likeId))
                .ReturnsAsync(true); // Simulate like exists

            _mockLikeRepository
                .Setup(repo => repo.DeleteLikeAsync(likeId))
                .ReturnsAsync(true); // Simulate successful deletion

            // Act
            var result = await _likeService.DeleteLikeAsync(likeId);

            // Assert
            Assert.True(result);  // Like exists and was successfully deleted, should return true
        }
    }

