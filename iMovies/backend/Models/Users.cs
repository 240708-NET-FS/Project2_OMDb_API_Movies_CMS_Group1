namespace OMDBProject.DTO;

public class Users
{
    public int UserID {get;set;}
    public string Username {get;set;}
    public string PasswordHash{get;set;}
    public DateTime CreatedAt{get;set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}

}