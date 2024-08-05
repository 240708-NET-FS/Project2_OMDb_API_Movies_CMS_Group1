using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs;


public class LikeDTO
{
    public int UserId { get; set; }
    public int UserMovieId { get; set; }
}