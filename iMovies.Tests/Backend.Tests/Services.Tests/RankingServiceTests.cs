using Moq;
using Xunit;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMDbProject.Tests;

    public class RankingServiceTests
    {
        private readonly RankingService _rankingService;
        private readonly Mock<IRankingsRepository> _mockRankingsRepository;

        public RankingServiceTests()
        {
            // Initialize the mock repository
            _mockRankingsRepository = new Mock<IRankingsRepository>();

            // Initialize the RankingService with the mock repository
            _rankingService = new RankingService(_mockRankingsRepository.Object);
        }

        [Fact]
        public async Task GetTopRankedMoviesAsync_ShouldReturnListOfMovies()
        {
            // Arrange
            var topRankedMovies = new List<MovieRankDTO>
            {
                new MovieRankDTO { OMDBId = "tt1234567", NumberOfLikes = 100, NumberofAdds = 50, NumberOfRatings = 200, AverageRating = 8.5m },
                new MovieRankDTO { OMDBId = "tt7654321", NumberOfLikes = 150, NumberofAdds = 75, NumberOfRatings = 300, AverageRating = 9.0m }
            };

            // Setup the mock to return the list of movies when GetTopRankedMoviesAsync is called
            _mockRankingsRepository
                .Setup(repo => repo.GetTopRankedMoviesAsync())
                .ReturnsAsync(topRankedMovies);

            // Act
            var result = await _rankingService.GetTopRankedMoviesAsync();

            // Assert
            Assert.NotNull(result);  // Ensure the result is not null
            Assert.Equal(2, result.Count);  // Assert that the result contains 2 movies
        }
    }

