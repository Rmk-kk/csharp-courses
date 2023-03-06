namespace NetCoreCourse.Models;

public class Course : BaseModel
{
    public string Name {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public CourseStatus Status {get; set;}
    public int CourseSize {get; set;}
    
    public enum CourseStatus
    {
        NotStarted,
        Ongoing,
        Ended
    }
}