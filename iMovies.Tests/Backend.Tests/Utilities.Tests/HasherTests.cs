using Moq;
using Xunit;
using OMDbProject.Models;
using OMDbProject.Utilities;
using OMDbProject.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using System;

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

    [Fact]
    public void HashPassword_ShouldHandleEmptyPassword()
    {
        // Arrange
        string password = "";
        string salt = _hasher.GenerateSalt();

        // Act
        string hash = _hasher.HashPassword(password, salt);

        // Assert
        Assert.NotNull(hash);
        Assert.NotEmpty(hash);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_ForEmptyPassword()
    {
        // Arrange
        string password = "";
        string wrongPassword = "SomePassword";
        string salt = _hasher.GenerateSalt();
        string hash = _hasher.HashPassword(password, salt);

        // Act
        bool result = _hasher.VerifyPassword(wrongPassword, hash, salt);

        // Assert
        Assert.False(result);
    }

    public bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        if (storedSalt == null)
        {
            return false; // Handle null salt case here
        }

        // Convert the base64 encoded salt to a byte array
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        // Compute the hash for the given password and salt
        string computedHash = _hasher.HashPassword(password, storedSalt);

        // Compare the computed hash with the stored hash
        return computedHash == storedHash;
    }


    [Fact]
    public void GenerateJwtToken_ShouldThrowExceptionForNullUser()
    {
        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _hasher.GenerateJwtToken(null));
    }


    [Fact]
    public void HashPassword_ShouldThrowFormatException_ForInvalidBase64Salt()
    {
        // Arrange
        string password = "TestPassword";
        string invalidSalt = "InvalidBase64Salt";

        // Act & Assert
        Assert.Throws<FormatException>(() => _hasher.HashPassword(password, invalidSalt));
    }

    [Fact]
    public void VerifyPassword_ShouldThrowArgumentNullException_ForNullSalt()
    {
        // Arrange
        string password = "TestPassword";
        string storedHash = _hasher.HashPassword(password, _hasher.GenerateSalt());
        string nullSalt = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _hasher.VerifyPassword(password, storedHash, nullSalt));
        Assert.Equal("Salt cannot be null or empty. (Parameter 'storedSalt')", exception.Message);
    }


    [Fact]
    public void VerifyPassword_ShouldReturnFalse_ForNullHash()
    {
        // Arrange
        string password = "TestPassword";
        string salt = _hasher.GenerateSalt();
        string nullHash = null;

        // Act
        bool result = _hasher.VerifyPassword(password, nullHash, salt);

        // Assert
        Assert.False(result);
    }



    [Fact]
    public void GenerateJwtToken_ShouldHandleZeroExpiry()
    {
        // Arrange
        var user = new User { UserName = "TestUser" };
        _mockJwtSettings.SetupGet(m => m.Value).Returns(new JwtSettings
        {
            Secret = "ThisIsASecretKeyForTesting123Thishastobelongenough",
            ExpiryInMinutes = 0
        });

        // Act
        string token = _hasher.GenerateJwtToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }


    [Fact]
    public void GenerateJwtToken_ShouldHandleDifferentSecretKeyLengths()
    {
        // Arrange
        var user = new User { UserName = "TestUser" };
        _mockJwtSettings.SetupGet(m => m.Value).Returns(new JwtSettings
        {
            Secret = "ShortKey", // Shorter key for testing
            ExpiryInMinutes = 60
        });

        // Act
        string token = _hasher.GenerateJwtToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }




    [Fact]
    public void VerifyPassword_ShouldThrowFormatException_ForInvalidBase64Salt()
    {
        // Arrange
        string password = "TestPassword";
        string invalidBase64Salt = "InvalidBase64Salt"; // Not a valid Base64 string
        string hash = _hasher.HashPassword(password, _hasher.GenerateSalt());

        // Act & Assert
        Assert.Throws<FormatException>(() => _hasher.VerifyPassword(password, hash, invalidBase64Salt));
    }


    [Fact]
    public void HashPassword_ShouldThrowFormatException_ForEmptySalt()
    {
        // Arrange
        string password = "TestPassword";
        string emptySalt = "";

        // Act & Assert
        Assert.Throws<FormatException>(() => _hasher.HashPassword(password, emptySalt));
    }






}

