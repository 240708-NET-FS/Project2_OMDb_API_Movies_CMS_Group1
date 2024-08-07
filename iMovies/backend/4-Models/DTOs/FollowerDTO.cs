using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;

public class FollowerDTO
{
    public int UserId { get; set; }
    public int FollowerUserId { get; set; }
    public DateTime CreatedAt {get; set;}
}