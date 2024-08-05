using System.ComponentModel.DataAnnotations;

namespace OMDBProject.DTO;

public class Followers
{
    [Key]
    public int FollowerID {get;set;}
    //One-To-Many
    public int UserID {get;set;}
    //One-To-Many
    public int FollowerUserID{get;set;}
    public DateTime CreatedAt{get;set;}
}