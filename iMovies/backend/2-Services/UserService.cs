using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Utilities.Interfaces;
using System.Security.Cryptography;
using System.Text;


namespace OMDbProject.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHasher _hasher;

    public UserService(IUserRepository userRepository, IHasher hasher)
    {
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task<UserRegistrationResponseDTO> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO)
    {
        //added to handle null value
        if (userRegistrationDTO == null)
            throw new ArgumentNullException(nameof(userRegistrationDTO));

        var existingUser = await _userRepository.GetUserByUserNameAsync(userRegistrationDTO.UserName);

        if (existingUser != null)
        {
            throw new Exception("Username is already taken.");
        }


        // Generate salt and hash the password using the Hasher class
        var salt = _hasher.GenerateSalt();
        var hashedPassword = _hasher.HashPassword(userRegistrationDTO.Password, salt);


        var user = new User
        {
            FirstName = userRegistrationDTO.FirstName,
            LastName = userRegistrationDTO.LastName,
            UserName = userRegistrationDTO.UserName,
            PasswordHash = hashedPassword,
            Salt = salt,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddUserAsync(user); //add user
        var addedUser = await _userRepository.GetUserByUserNameAsync(user.UserName); //return added user details


        if (addedUser == null)
            throw new Exception("User was not added correctly.");

        return new UserRegistrationResponseDTO
        {
            UserId = addedUser.UserId,
            UserName = addedUser.UserName,
            FirstName = addedUser.FirstName,
            LastName = addedUser.LastName,
            CreatedAt = addedUser.CreatedAt
        };

    }

    public async Task<List<UserWithMoviesDTO>> GetAllUsersWithMoviesAsync()
    {
        return await _userRepository.GetAllUsersWithMoviesAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }




}


