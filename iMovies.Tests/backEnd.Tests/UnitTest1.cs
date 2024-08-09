using Moq;
using OMDbProject.Repositories;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Services;
using OMDbProject.Services.Interfaces;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace backend.Tests;

public class UnitTest1
{

    private readonly ITestOutputHelper output;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UserService _userService;
 

    public UnitTest1(ITestOutputHelper output)
    {
        this.output = output; // <- Output setup.
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

[Fact]
    public async Task Test_UserAlreadyExists_ShouldReturnUserNameAlreadyTaken()
    {
        // Arrange
        var userRegistrationDTO = new UserRegistrationDTO
        {
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe",
            Password = "securepassword"
        };
        
        var expectedUser = new User
        {
            UserId = 0,
            FirstName = "John",
            LastName = "Doe",
            UserName = "johndoe",
            PasswordHash = "hashedpassword",
            Salt = "somesalt",
            CreatedAt = DateTime.UtcNow
        };
    
    
        // Assert
        // Setup the repository to return an existing user when searched by the username
        _mockUserRepository
            .Setup(repo => repo.GetUserByUserNameAsync("johndoe"))
            .ReturnsAsync(expectedUser);
 
        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _userService.RegisterUserAsync(userRegistrationDTO));
    
        // Verify that the exception message is "Username is already taken."
        Assert.Equal("Username is already taken.", exception.Message);
    }
}