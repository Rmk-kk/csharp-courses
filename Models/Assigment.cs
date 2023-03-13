namespace NetCoreCourse.Models;

public class Assigment : BaseModel
{
    public DateTime Deadline {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;

    public ICollection<Student> Students {get; set;} = null!;
}