using System;
using System.Threading.Tasks;
using OMDbProject.Models.DTOs;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using OMDbProject.Utilities;
using OMDbProject.Utilities.Interfaces;

namespace OMDbProject.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;

    public AuthService(IUserRepository userRepository, IHasher hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }


    public async Task<UserResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        // Retrieve the user from the database
        var user = await _userRepository.GetUserByUserNameAsync(loginDTO.UserName);


        if (user == null)
        {
            // User does not exist
            throw new UnauthorizedAccessException("Invalid username or password.");
        }


        // Retrieve the stored password hash and salt
        var storedHash = user.PasswordHash;
        var storedSalt = user.Salt;

        // Verify the provided password
        if (!_hasher.VerifyPassword(loginDTO.Password, storedHash, storedSalt))
        {
            // Password does not match
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        //Generate and return JWT token if password is correct

        var JwtToken = _hasher.GenerateJwtToken(user);


        return new UserResponseDTO
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            Token = JwtToken
        };
    }


    public Task LogoutAsync()
    {

        //Invalidate the current user's JWT on the client side
        return Task.CompletedTask;
    }



}

