namespace NetCoreCourse.Models;

public class Student : BaseModel
{
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    public string Email {get; set;} = null!;
}