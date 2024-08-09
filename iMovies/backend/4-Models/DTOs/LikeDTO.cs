using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;


public class LikeDTO
{
    public int LikeId { get; set;}
    public int UserId { get; set; }
    public int UserMovieId { get; set; }
    public DateTime CreatedAt {get; set;}
}