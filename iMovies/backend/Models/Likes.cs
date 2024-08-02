namespace OMDBProject.DTO;

public class Likes
{
    public int LikeID {get;set;}
    //One-To-Many
    public int UserID {get;set;}
    //One-To-Many
    public int UserMovieID{get;set;}
    public DateTime CreatedAt{get;set;}
}