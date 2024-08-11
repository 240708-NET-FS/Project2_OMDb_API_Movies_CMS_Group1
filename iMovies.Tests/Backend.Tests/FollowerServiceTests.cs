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

    public class FollowerServiceTests
    {
        private readonly FollowerService _followerService;
        private readonly Mock<IFollowerRepository> _mockFollowerRepository;

        public FollowerServiceTests()
        {
            _mockFollowerRepository = new Mock<IFollowerRepository>();
            _followerService = new FollowerService(_mockFollowerRepository.Object);
        }

        [Fact]
        public async Task AddFollowerAsync_ShouldReturnNull_WhenUserTriesToFollowThemselves()
        {
            // Arrange
            var followerDTO = new FollowerDTO { UserId = 1, FollowerUserId = 1 };

            // Act
            var result = await _followerService.AddFollowerAsync(followerDTO);

            // Assert
            Assert.Null(result);  // User cannot follow themselves, should return null
        }

        [Fact]
        public async Task AddFollowerAsync_ShouldReturnFollower_WhenSuccessfullyAdded()
        {
            // Arrange
            var followerDTO = new FollowerDTO { UserId = 1, FollowerUserId = 2 };
            var follower = new Follower { UserId = 1, FollowerUserId = 2, CreatedAt = DateTime.UtcNow };

            _mockFollowerRepository
                .Setup(repo => repo.AddFollowerAsync(It.IsAny<Follower>()))
                .ReturnsAsync(follower);

            // Act
            var result = await _followerService.AddFollowerAsync(followerDTO);

            // Assert
            Assert.NotNull(result);  // Follower should be returned
            Assert.Equal(follower.UserId, result.UserId);
            Assert.Equal(follower.FollowerUserId, result.FollowerUserId);
        }

        [Fact]
        public async Task GetFollowersByUserIdAsync_ShouldReturnFollowersList()
        {
            // Arrange
            var userId = 1;
            var followers = new List<Follower>
            {
                new Follower { UserId = userId, FollowerUserId = 2, CreatedAt = DateTime.UtcNow },
                new Follower { UserId = userId, FollowerUserId = 3, CreatedAt = DateTime.UtcNow }
            };

            _mockFollowerRepository
                .Setup(repo => repo.GetFollowersByUserIdAsync(userId))
                .ReturnsAsync(followers);

            // Act
            var result = await _followerService.GetFollowersByUserIdAsync(userId);

            // Assert
            Assert.Equal(2, result.Count());  // Ensure the result contains 2 followers
        }

        [Fact]
        public async Task DeleteFollowingRelationshipAsync_ShouldReturnTrue_WhenSuccessfullyDeleted()
        {
            // Arrange
            var id = 1;
            _mockFollowerRepository
                .Setup(repo => repo.DeleteFollowingRelationshipAsync(id))
                .ReturnsAsync(true);

            // Act
            var result = await _followerService.DeleteFollowingRelationshipAsync(id);

            // Assert
            Assert.True(result);  // Deletion should be successful
        }

        [Fact]
        public async Task GetFollowingWithMoviesAsync_ShouldReturnUserWithMoviesList()
        {
            // Arrange
            var userId = 1;
            var userWithMovies = new List<UserWithMoviesDTO>
            {
                new UserWithMoviesDTO { UserName = "User1" },
                new UserWithMoviesDTO { UserName = "User2" }
            };

            _mockFollowerRepository
                .Setup(repo => repo.GetFollowingWithMoviesAsync(userId))
                .ReturnsAsync(userWithMovies);

            // Act
            var result = await _followerService.GetFollowingWithMoviesAsync(userId);

            // Assert
            Assert.Equal(2, result.Count);  // Ensure the result contains 2 users with movies
        }
    }

