using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories;
using OMDbProject.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;

namespace OMDbProject.Services;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserRegistrationResponseDTO> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var existingUser = await _userRepository.GetUserByUserNameAsync(userRegistrationDTO.UserName);

            if (existingUser != null)
            {
                throw new Exception("Username is already taken.");
            }


            // Generate salt and hash the password
            var salt = GenerateSalt();
            var hashedPassword = HashPassword(userRegistrationDTO.Password, salt);


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
            
            return new UserRegistrationResponseDTO
            {
                UserId = addedUser.UserId, 
                UserName = addedUser.UserName,
                FirstName = addedUser.FirstName,
                LastName = addedUser.LastName,
                CreatedAt = addedUser.CreatedAt 
             };

        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }


        //Methods for password hashing: generate salt and then hash

         private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

    
    private string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var hmac = new HMACSHA512(saltBytes);
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }


}


