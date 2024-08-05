using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;

public class UserRegistrationDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}