namespace Backend.Tests;

using OMDbProject.Services;
using OMDbProject.Repositories;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Utilities.Interfaces;
using OMDbProject.Models.DTOs;
using OMDbProject.Models;
using Moq;
using Xunit;
using System.Threading.Tasks;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IHasher> _mockHasher;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockHasher = new Mock<IHasher>();
        _authService = new AuthService(_mockUserRepository.Object, _mockHasher.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnUserResponseDTO_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDTO = new LoginDTO { UserName = "validUser", Password = "validPassword" };

        var user = new User
        {
            UserId = 1,
            UserName = loginDTO.UserName,
            PasswordHash = "hashedPassword",
            Salt = "salt"
        };

        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(loginDTO.UserName))
            .ReturnsAsync(user);

        _mockHasher.Setup(hasher => hasher.VerifyPassword(loginDTO.Password, user.PasswordHash, user.Salt))
            .Returns(true);

        _mockHasher.Setup(hasher => hasher.GenerateJwtToken(user))
            .Returns("generatedJwtToken");

        // Act
        var result = await _authService.LoginAsync(loginDTO);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.UserName, result.UserName);
        Assert.Equal("generatedJwtToken", result.Token);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenUserNameIsNotFound()
    {
        // Arrange
        var loginDTO = new LoginDTO { UserName = "nonExistentUser", Password = "password" };

        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(loginDTO.UserName))
            .ReturnsAsync((User)null);

        // Act
        var exception = await Record.ExceptionAsync(() => _authService.LoginAsync(loginDTO));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<UnauthorizedAccessException>(exception);
        Assert.Equal("Invalid username or password.", exception.Message);
    }



    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenPasswordIsInvalid()
    {
        // Arrange
        var loginDTO = new LoginDTO { UserName = "validUser", Password = "invalidPassword" };

        var user = new User
        {
            UserId = 1,
            UserName = loginDTO.UserName,
            PasswordHash = "hashedPassword",
            Salt = "salt"
        };

        _mockUserRepository.Setup(repo => repo.GetUserByUserNameAsync(loginDTO.UserName))
            .ReturnsAsync(user);

        _mockHasher.Setup(hasher => hasher.VerifyPassword(loginDTO.Password, user.PasswordHash, user.Salt))
            .Returns(false);

        // Act
        var exception = await Record.ExceptionAsync(() => _authService.LoginAsync(loginDTO));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<UnauthorizedAccessException>(exception);
        Assert.Equal("Invalid username or password.", exception.Message);
    }

}
