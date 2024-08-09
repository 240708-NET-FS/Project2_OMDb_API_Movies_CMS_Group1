namespace OMDbProject.Models.DTOs;
public class UserWithMoviesDTO
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int FollowingRelationshipId { get; set; } 
    public List<UserMovieDTO> UserMovies { get; set; } = new List<UserMovieDTO>();
}
