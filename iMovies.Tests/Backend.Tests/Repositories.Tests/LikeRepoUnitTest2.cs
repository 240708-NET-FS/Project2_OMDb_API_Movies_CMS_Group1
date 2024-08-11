using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Repositories;

public class LikeRepoUnitTest2
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public LikeRepoUnitTest2()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase2" + Guid.NewGuid())
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
    public async Task LikeExistsAsync_LikeDoesNotExist_ReturnsFalse()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = new ApplicationDbContext(_options);
        var likeRepository = new LikeRepository(context);

        int likeId = 9999;

        // Act
        var result = await likeRepository.LikeExistsAsync(likeId);

        // Assert
        Assert.False(result);
    }


}
