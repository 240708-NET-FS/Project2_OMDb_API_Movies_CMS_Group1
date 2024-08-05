using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

