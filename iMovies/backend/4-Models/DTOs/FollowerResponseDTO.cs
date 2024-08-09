using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;

public class FollowerResponseDTO
{
    public int FollowingRelationshipId { get; set;}
    public int UserId { get; set; }
    public int FollowerUserId { get; set; }
    public DateTime CreatedAt {get; set;}
}