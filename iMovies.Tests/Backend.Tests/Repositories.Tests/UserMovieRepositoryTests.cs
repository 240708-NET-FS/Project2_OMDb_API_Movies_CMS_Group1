using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Repositories;

public class UserMovieRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public UserMovieRepositoryTests()
    {
        // Setup the options for DbContext
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid())  // Ensure unique database per test run
            .Options;
    }

    private ApplicationDbContext CreateContext()
    {
        return new ApplicationDbContext(_options);
    }

    private void SeedDatabase()
    {
        using var context = CreateContext();
        context.Database.EnsureDeleted();  // Ensure the database is empty
        context.Database.EnsureCreated();  // Create the database

        var userMovies = new List<UserMovie>
        {
            new UserMovie { UserMovieId = 1, OMDBId = "456", UserId = 1 },
            new UserMovie { UserMovieId = 2, OMDBId = "567", UserId = 1 },
            new UserMovie { UserMovieId = 3, OMDBId = "678", UserId = 2 }
        };

        context.UserMovies.AddRange(userMovies);
        context.SaveChanges();
    }

    [Fact]
    public async Task AddUserMovieAsync_ValidUserMovie_ReturnsUserMovie()
    {
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);

        // Arrange
        var userMovie = new UserMovie { UserMovieId = 4, OMDBId = "1002", UserId = 3 };

        // Act
        var result = await repository.AddUserMovieAsync(userMovie);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userMovie.UserMovieId, result.UserMovieId);
        Assert.Equal(userMovie.OMDBId, result.OMDBId);
    }

    [Fact]
    public async Task UpdateUserMovieAsync_ExistingUserMovie_ReturnsUpdatedUserMovie()
    {
        // Arrange

        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);

        // First, retrieve the existing UserMovie by its ID
        var userMovieToUpdate = await repository.GetUserMovieByIdAsync(1);

        // Check if the userMovieToUpdate is not null (important for avoiding null reference exceptions)
        Assert.NotNull(userMovieToUpdate);

        // Update the properties of the retrieved UserMovie
        userMovieToUpdate.OMDBId = "1239098";
        userMovieToUpdate.UserReview = "update from test method";

        // Act
        var result = await repository.UpdateUserMovieAsync(userMovieToUpdate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userMovieToUpdate.UserMovieId, result.UserMovieId);
        Assert.Equal("1239098", result.OMDBId);
        Assert.Equal("update from test method", result.UserReview);
    }



    [Fact]
    public async Task GetUserMovieByIdAsync_UserMovieExists_ReturnsUserMovie()
    {
        // Arrange

        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);

        // Declare and initialize UserMovie id

        int id = 1;

        // Act
        var result = await repository.GetUserMovieByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.UserMovieId);
    }



    [Fact]
    public async Task GetUserMovieByIdAsync_UserMovieDoesNotExist_ReturnsNull()
    {
        // Arrange

        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);


        //Declare and initialize UserMovie id
        int id = 999;

        // Act
        var result = await repository.GetUserMovieByIdAsync(id);

        // Assert: will return null
        Assert.Null(result);
    }



    [Fact]
    public async Task GetAllUserMoviesAsync_ReturnsAllUserMovies()
    {

        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);

        // Act
        var result = await repository.GetAllUserMoviesAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count());
    }


    [Fact]
    public async Task GetUserMoviesByUserIdAsync_UserMoviesExist_ReturnsUserMovies()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);

        //Declare and initialize user id
        int userId = 1;

        // Act
        var result = await repository.GetUserMoviesByUserIdAsync(userId);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, um => Assert.Equal(userId, um.UserId));
    }

    [Fact]
    public async Task GetUserMoviesByUserIdAsync_NoUserMoviesExist_ReturnsEmpty()
    {
        // Arrange
        // Seed database before test
        SeedDatabase();

        // Use a single context instance throughout the test
        using var context = CreateContext();
        var repository = new UserMovieRepository(context);

        //Declare and initialize userId
        int userId = 999;

        // Act
        var result = await repository.GetUserMoviesByUserIdAsync(userId);

        // Assert
        Assert.Empty(result);
    }


}

