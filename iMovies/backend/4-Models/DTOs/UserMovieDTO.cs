using System.ComponentModel.DataAnnotations;

namespace OMDbProject.Models.DTOs
public class UserMovieDTO
{
    public int UserId { get; set; }
    public string OMDBId { get; set; }
    public DateTime WatchedOn { get; set; }
    public decimal UserRating { get; set; }
    public string UserReview { get; set; }
}