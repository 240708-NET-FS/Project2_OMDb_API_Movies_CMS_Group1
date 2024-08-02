namespace OMDBProject.DTO;

public class Followers
{
    public int FollowerID {get;set;}
    //One-To-Many
    public int UserID {get;set;}
    //One-To-Many
    public int FollowerUserID{get;set;}
    public DateTime CreatedAt{get;set;}
}