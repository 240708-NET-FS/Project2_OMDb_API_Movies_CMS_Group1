using Microsoft.EntityFrameworkCore;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

public class FollowerRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public FollowerRepositoryTests()
    {
        // Set up an in-memory database for each test
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use a unique database name for each test run
            .Options;
    }

    private ApplicationDbContext CreateContext()
    {
        // Create a new instance of ApplicationDbContext with the in-memory database options
        var context = new ApplicationDbContext(_dbContextOptions);
        
        // Seed data if necessary
        context.Followers.Add(
            new Follower
            {
                FollowingRelationshipId = 1,
                UserId = 1,
                FollowerUserId = 2,
                CreatedAt = DateTime.UtcNow
            }
        
        );

    context.Followers.Add(
            new Follower
            {
                FollowingRelationshipId = 3,
                UserId = 2,
                FollowerUserId = 1,
                CreatedAt = DateTime.UtcNow
            }
        
        );



    // Declare variables and initialize them with UserMovie instances
    var userMovie1 = new UserMovie
    {
        UserId = 2,
        OMDBId = "1"
    };

    var userMovie2 = new UserMovie
    {
        UserId = 2,
        OMDBId = "2"
    };

    // Add UserMovie instances to the context
    context.UserMovies.AddRange(userMovie1, userMovie2);


     // Declare and initialize User instances
    var user1 = new User
    {
        UserId = 1,
        FirstName = "Owusu",
        LastName = "Achamfour",
        PasswordHash = "34234asfff4234asdf",
        Salt = "sdfsdfsdf",
        UserName = "user1",
        UserMovies = new List<UserMovie>
        {
            userMovie1 // Assuming userMovie1 is associated with this user
        }
    };

    var user2 = new User
    {
        UserId = 2,
        FirstName = "John",
        LastName = "Doe",
        PasswordHash = "3fdgsd8%$4asfff4234asdf",
        Salt = "fopasdfasdf",
        UserName = "user2",   
        UserMovies = new List<UserMovie>
        {
            userMovie1,
            userMovie2 // Assuming userMovie2 is associated with this user
        }
    };

    // Add User instances to the context
    context.Users.AddRange(user1, user2);
   


    // Save changes to the in-memory database
    context.SaveChanges();

        return context;
    }



    [Fact]
    public async Task AddFollowerAsync_ShouldAddFollower_WhenFollowerDoesNotExist()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new FollowerRepository(context);

        var newFollower = new Follower
        {
            FollowingRelationshipId = 2,
            UserId = 1,
            FollowerUserId = 3,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await repository.AddFollowerAsync(newFollower);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newFollower.FollowingRelationshipId, result.FollowingRelationshipId);
        var followers = await repository.GetFollowersByUserIdAsync(newFollower.UserId);
        Assert.Contains(newFollower, followers);
    }

    [Fact]
    public async Task AddFollowerAsync_ShouldReturnNull_WhenFollowerExists()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new FollowerRepository(context);

        var existingFollower = new Follower
        {
            FollowingRelationshipId = 1,
            UserId = 1,
            FollowerUserId = 2,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var result = await repository.AddFollowerAsync(existingFollower);

        // Assert
        Assert.Null(result); // Assuming that your method returns null when a follower already exists
    }

    [Fact]
    public async Task DeleteFollowingRelationshipAsync_ShouldReturnFalse_WhenFollowerDoesNotExist()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new FollowerRepository(context);

        // Act
        var result = await repository.DeleteFollowingRelationshipAsync(999); // Assuming 999 does not exist

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetFollowersByUserIdAsync_ShouldReturnFollowers_WhenFollowersExist()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new FollowerRepository(context);

        var userId = 1;

        // Act
        var result = await repository.GetFollowersByUserIdAsync(userId);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, f => Assert.Equal(userId, f.UserId));
    }

    
    [Fact]
    public async Task GetFollowingWithMoviesAsync_ShouldReturnUsersWithMovies_WhenFollowingExists()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new FollowerRepository(context);

        var userId = 1;

        // Act
        var result = await repository.GetFollowingWithMoviesAsync(userId);

        // Assert
        Assert.NotEmpty(result);

        // Verify that each user in the result has movies
        foreach (var user in result)
        {
            Assert.NotEmpty(user.UserMovies);
        }
    }

}
