using System.Threading.Tasks;
using Moq;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Services;
using Microsoft.Extensions.Options;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IOptions<JwtSettings>> _mockJwtSettings;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
        _mockJwtSettings.SetupGet(x => x.Value).Returns(new JwtSettings
        {
            Secret = "your_secret_key",
            ExpiryInMinutes = 60
        });

        _authService = new AuthService(_mockUserRepository.Object, _mockJwtSettings.Object);
    }

    [Fact]
    public async Task LogoutAsync_ShouldCompleteSuccessfully()
    {
        // Act
        await _authService.LogoutAsync();

        // Assert
        // No exceptions should be thrown, so we use Assert.True to check successful completion
        Assert.True(true);
    }
}
