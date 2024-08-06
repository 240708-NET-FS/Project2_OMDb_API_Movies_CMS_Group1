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

namespace OMDbProject.Services;

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

         public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }


public async Task<string> LoginAsync(LoginDTO loginDTO)
{
    // Retrieve the user from the database
    var user = await _userRepository.GetUserByUserNameAsync(loginDTO.UserName);
    Console.WriteLine("user.UserName:"+ user.UserName);

    if (user == null)
    {
        // User does not exist
        throw new UnauthorizedAccessException("Invalid username or password.");
    }

    // Retrieve the stored password hash and salt
    var storedHash = user.PasswordHash;
    var storedSalt = user.Salt;

    Console.WriteLine("storedHash:" + storedHash);
    Console.WriteLine("storedSalt:" + storedSalt);

    // Verify the provided password
    if (!VerifyPassword(loginDTO.Password, storedHash, storedSalt))
    {
        // Password does not match
        throw new UnauthorizedAccessException("Invalid username or password.");
    }

    // Generate and return JWT token if password is correct
    return GenerateJwtToken(user);
}


        public Task LogoutAsync()
        {
            
            //Invalidate the current user's JWT on the client side
            return Task.CompletedTask;
        }


    
    //Helper Methods
   
    private bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        Console.WriteLine("saltBytes: " + saltBytes);
        using var hmac = new HMACSHA512(saltBytes);
        Console.WriteLine("hmac: " + hmac);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        Console.WriteLine("computedHash: " + computedHash);
        string computedHashString = Convert.ToBase64String(computedHash);
        Console.WriteLine("computedHashString:" + computedHashString);
        return computedHashString == storedHash;
    }


        private string GenerateJwtToken(User user)
    {
        Console.WriteLine("GenerateJwtToken() is running");
    
        var tokenHandler = new JwtSecurityTokenHandler();
        Console.WriteLine("tokenHandler: " + tokenHandler);

        Console.WriteLine("JWT Secret: " + _jwtSettings.Secret);

        //GetBytes(): This has to return at least 32 bytes. 
        //And bytes do not necessarily equal number of characters 
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret); 
    
        Console.WriteLine("key (Base64): " + Convert.ToBase64String(key));

        var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            }),
        Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        Console.WriteLine("token from GenerateJwtToken(): " + token);
        return tokenHandler.WriteToken(token);
    }
}

