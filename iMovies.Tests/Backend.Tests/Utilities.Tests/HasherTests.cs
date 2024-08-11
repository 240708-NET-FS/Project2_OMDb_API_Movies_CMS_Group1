using OMDbProject.Models;
using OMDbProject.Utilities;
using OMDbProject.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace OMDbProject.Tests.Utilities;

public class HasherTests
{
    private readonly Hasher _hasher;
    private readonly Mock<IOptions<JwtSettings>> _mockJwtSettings;

    public HasherTests()
    {
        _mockJwtSettings = new Mock<IOptions<JwtSettings>>();
        _mockJwtSettings.SetupGet(m => m.Value).Returns(new JwtSettings
        {
            Secret = "ThisIsASecretKeyForTesting123Thishastobelongenough", // Should be at least 32 bytes long
            ExpiryInMinutes = 60
        });

        _hasher = new Hasher(_mockJwtSettings.Object);
    }

    [Fact]
    public void GenerateSalt_ShouldReturnNonNullOrEmptyString()
    {
        // Act
        string salt = _hasher.GenerateSalt();

        // Assert
        Assert.NotNull(salt);
        Assert.NotEmpty(salt);
        Assert.Equal(16, Convert.FromBase64String(salt).Length); // 16 bytes
    }

    [Fact]
    public void HashPassword_ShouldReturnHash()
    {
        // Arrange
        string password = "TestPassword";
        string salt = _hasher.GenerateSalt();

        // Act
        string hash = _hasher.HashPassword(password, salt);

        // Assert
        Assert.NotNull(hash);
        Assert.NotEmpty(hash);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_ForValidPassword()
    {
        // Arrange
        string password = "TestPassword";
        string salt = _hasher.GenerateSalt();
        string hash = _hasher.HashPassword(password, salt);

        // Act
        bool result = _hasher.VerifyPassword(password, hash, salt);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_ForInvalidPassword()
    {
        // Arrange
        string password = "TestPassword";
        string wrongPassword = "WrongPassword";
        string salt = _hasher.GenerateSalt();
        string hash = _hasher.HashPassword(password, salt);

        // Act
        bool result = _hasher.VerifyPassword(wrongPassword, hash, salt);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GenerateJwtToken_ShouldReturnToken()
    {
        // Arrange
        var user = new User { UserName = "TestUser" };

        // Act
        string token = _hasher.GenerateJwtToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }
}

