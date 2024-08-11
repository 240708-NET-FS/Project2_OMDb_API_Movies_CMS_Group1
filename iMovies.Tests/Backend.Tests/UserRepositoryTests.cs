using Microsoft.EntityFrameworkCore;
using OMDbProject.Models;
using OMDbProject.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OMDbProject.Tests.Repositories;

    public class UserRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public UserRepositoryTests()
        {
            // Set up the in-memory database options
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryUserDatabase")
                .Options;

            // Seed data for the in-memory database
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                if (!context.Users.AnyAsync().Result)
                {
                    // Seed Users
                    var users = new List<User>
                    {
                        new User
                        {
                            UserId = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            UserName = "johndoe",
                            PasswordHash = "hashedpassword1",
                            Salt = "salt1",
                            CreatedAt = DateTime.UtcNow
                        },
                        new User
                        {
                            UserId = 2,
                            FirstName = "Jane",
                            LastName = "Doe",
                            UserName = "janedoe",
                            PasswordHash = "hashedpassword2",
                            Salt = "salt2",
                            CreatedAt = DateTime.UtcNow
                        },

                            new User
                        {
                            UserId = 3,
                            FirstName = "Jane",
                            LastName = "Doe",
                            UserName = "existingUser",
                            PasswordHash = "hashedpassword2",
                            Salt = "salt2",
                            CreatedAt = DateTime.UtcNow
                        }
                    };

                    context.Users.AddRange(users);

                    // Seed UserMovies
                    var userMovies = new List<UserMovie>
                    {
                        new UserMovie
                        {
                            UserMovieId = 1,
                            UserId = 1,
                            OMDBId = "1",
                            UserRating = 5,
                            UserReview = "Great movie!",
                            CreatedAt = DateTime.UtcNow
                        },
                        new UserMovie
                        {
                            UserMovieId = 2,
                            UserId = 2,
                            OMDBId = "2",
                            UserRating = 4,
                            UserReview = "Good movie!",
                            CreatedAt = DateTime.UtcNow
                        }
                    };

                    context.UserMovies.AddRange(userMovies);

                    // Seed Followers
                    var followers = new List<Follower>
                    {
                        new Follower
                        {
                            FollowingRelationshipId = 1,
                            UserId = 1,
                            FollowerUserId = 2,
                            CreatedAt = DateTime.UtcNow
                        }
                    };

                    context.Followers.AddRange(followers);

                    // Save changes
                    context.SaveChanges();
                }
            }
        }

        private ApplicationDbContext CreateContext()
        {
            return new ApplicationDbContext(_dbContextOptions);
        }



        private ApplicationDbContext CreateEmptyContext()
{
    // Create a new instance of ApplicationDbContext with the in-memory database options
    var context = new ApplicationDbContext(_dbContextOptions);

    // Ensure the database is empty
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    return context;
}


        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var context = CreateContext();
            var repository = new UserRepository(context);
            var userId = 1; // Assuming a user with UserId = 1 exists

            // Act
            var result = await repository.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
        }



                [Fact]
public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var nonExistingUserId = 999; // Assuming this ID does not exist

    // Act
    var result = await repository.GetUserByIdAsync(nonExistingUserId);

    // Assert
    Assert.Null(result);
}



[Fact]
public async Task GetUserByUserNameAsync_ShouldReturnUser_WhenUserNameExists()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var username = "existingUser"; // Assuming this username exists

    // Act
    var result = await repository.GetUserByUserNameAsync(username);

    // Assert
    Assert.NotNull(result);
}


[Fact]
public async Task GetUserByUserNameAsync_ShouldReturnNull_WhenUserNameDoesNotExist()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var nonExistingUsername = "nonExistingUser"; // Assuming this username does not exist

    // Act
    var result = await repository.GetUserByUserNameAsync(nonExistingUsername);

    // Assert
    Assert.Null(result);
}


[Fact]
public async Task AddUserAsync_ShouldAddUser_WhenUserIsValid()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var newUser = new User
    {
        UserName = "newUser",
        FirstName = "John",
        LastName = "Doe",
        PasswordHash = "hashedpassword",
        Salt = "srwer234wdsfdfsd",
        CreatedAt = DateTime.UtcNow
    };

    // Act
    await repository.AddUserAsync(newUser);
    var addedUser = await context.Users.FindAsync(newUser.UserId);

    // Assert
    Assert.NotNull(addedUser);
}


[Fact]
public async Task UpdateUserAsync_ShouldUpdateUser_WhenUserExists()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var existingUser = await context.Users.FirstAsync(); // Assume a user exists
    existingUser.FirstName = "UpdatedName";

    // Act
    await repository.UpdateUserAsync(existingUser);
    var updatedUser = await context.Users.FindAsync(existingUser.UserId);

    // Assert
    Assert.Equal("UpdatedName", updatedUser.FirstName);
}

[Fact]
public async Task DeleteUserAsync_ShouldRemoveUser_WhenUserExists()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var userId = 1; // Assuming a user with UserId = 1 exists

    // Act
    await repository.DeleteUserAsync(userId);
    var deletedUser = await context.Users.FindAsync(userId);

    // Assert
    Assert.Null(deletedUser);
}



[Fact]
public async Task DeleteUserAsync_ShouldNotThrowException_WhenUserDoesNotExist()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);
    var nonExistingUserId = 999; // Assuming this ID does not exist

    // Act
    var exception = await Record.ExceptionAsync(() => repository.DeleteUserAsync(nonExistingUserId));

    // Assert
    Assert.Null(exception);
}


[Fact]
public async Task GetAllUsersWithMoviesAsync_ShouldReturnUsersWithMovies_WhenUsersExist()
{
    // Arrange
    var context = CreateContext();
    var repository = new UserRepository(context);

    // Act
    var result = await repository.GetAllUsersWithMoviesAsync();

    // Assert
    Assert.NotEmpty(result);
}


[Fact]
public async Task GetAllUsersWithMoviesAsync_ShouldReturnEmptyList_WhenNoUsersExist()
{
    // Arrange
    var context = CreateEmptyContext(); // Create a context without users
    var repository = new UserRepository(context);

    // Act
    var result = await repository.GetAllUsersWithMoviesAsync();

    // Assert
    Assert.Empty(result);
}



}

