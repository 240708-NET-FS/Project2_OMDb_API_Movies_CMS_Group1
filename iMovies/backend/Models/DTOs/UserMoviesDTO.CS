namespace OMDBProject.DTO;

public class UserMoviesDTO
{
    [Key]
    public int UserMovieID {get;set;}
    //One-To-Many
    public int UserID {get;set;}
    public string OMDbId{get;set;}
    public DateTime WatchedOn{get;set;}
    public float UserRating{get;set;}
    public string UserReview{get;set;}
    public DateTime CreatedAt{get;set;}
    public DateTime UpdatedAt{get;set;}
}