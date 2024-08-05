using Microsoft.AspNetCore.Mvc;
using OMDbProject.Models;
using OMDbProject.Models.DTOs;
using OMDbProject.Services.Interfaces;
using OMDbProject.Repositories;
using OMDbProject.Repositories.Interfaces;
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

        public async Task<User> RegisterUserAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var existingUser = await _userRepository.GetUserByUserNameAsync(userRegistrationDTO.UserName);

            if (existingUser != null)
            {
                throw new Exception("Username is already taken.");
            }

            var user = new User
            {
                FirstName = userRegistrationDTO.FirstName,
                LastName = userRegistrationDTO.LastName,
                UserName = userRegistrationDTO.UserName,
                PasswordHash = HashPassword(userRegistrationDTO.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(user);

            return user;
        }

        private string HashPassword(string password)
        {
            string hashedPassword = "";
            // Password hashing logic here
            return hashedPassword; // return hashed password
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
    }


