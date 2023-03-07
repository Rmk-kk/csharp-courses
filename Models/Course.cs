namespace NetCoreCourse.Models;

using System.ComponentModel.DataAnnotations.Schema;

public class Course : BaseModel
{
    public string Name {get; set;} = null!;
    public string? Description {get; set;} 
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public CourseStatus Status {get; set;}

    //data annotations or attribute 
    [Column("course_size", TypeName = "smallint")]
    public int Size {get; set;}
    
    public enum CourseStatus
    {
        NotStarted,
        Ongoing,
        Ended
    }
}