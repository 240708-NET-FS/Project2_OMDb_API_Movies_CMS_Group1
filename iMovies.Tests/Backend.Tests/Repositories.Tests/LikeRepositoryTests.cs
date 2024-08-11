using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Repositories;

public class LikeRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public LikeRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase" + Guid.NewGuid())
           .Options;

    }

    private void SeedDatabase()
    {
        var context = new ApplicationDbContext(_options);

        // Ensure the database is empty
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();


        var likes = new List<Like>
        {
            new Like { LikeId = 1, UserId = 1, UserMovieId = 1 },
            new Like { LikeId = 2, UserId = 1, UserMovieId = 2 },
            new Like { LikeId = 3, UserId = 2, UserMovieId = 1 }
        };

        context.Likes.AddRange(likes);
        context.SaveChanges();
    }

    [Fact]
    public async Task AddLikeAsync_ValidLike_ReturnsTrue()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        var like = new Like { UserId = 4, UserMovieId = 3 };

        // Act
        var result = await likeRepository.AddLikeAsync(like);

        // Assert
        Assert.True(result);
    }



    [Fact]
    public async Task GetLikeByUserAndMovieAsync_LikeDoesNotExist_ReturnsNull()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        int userId = 999;
        int userMovieId = 999;

        // Act
        var result = await likeRepository.GetLikeByUserAndMovieAsync(userId, userMovieId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetLikesForUserMovieAsync_LikesExist_ReturnsLikes()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        int userMovieId = 1;

        // Act
        var result = await likeRepository.GetLikesForUserMovieAsync(userMovieId);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, l => Assert.Equal(userMovieId, l.UserMovieId));
    }

    [Fact]
    public async Task GetLikesForUserMovieAsync_NoLikesExist_ReturnsEmpty()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        int userMovieId = 999;

        // Act
        var result = await likeRepository.GetLikesForUserMovieAsync(userMovieId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task DeleteLikeAsync_LikeExists_ReturnsTrue()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        int likeId = 1; //assuming likeId 1 exists in InMemory

        // Act
        var result = await likeRepository.DeleteLikeAsync(likeId);

        // Assert
        Assert.True(result); //made false
        Assert.Null(await context.Likes.FindAsync(likeId));
    }

    [Fact]
    public async Task DeleteLikeAsync_LikeDoesNotExist_ReturnsFalse()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        int likeId = 999;

        // Act
        var result = await likeRepository.DeleteLikeAsync(likeId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task LikeExistsAsync_LikeExists_ReturnsTrue()
    {
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        // Declare and initialize LikeId
        int likeId = 1;

        // Act
        var result = await likeRepository.LikeExistsAsync(likeId);

        // Assert
        Assert.True(result);
    }


}
