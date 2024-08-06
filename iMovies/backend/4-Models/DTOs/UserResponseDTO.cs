using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;

public class UserResponseDTO
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; } // JWT token
}
