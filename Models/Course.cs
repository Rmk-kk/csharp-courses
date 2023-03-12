namespace NetCoreCourse.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Course : BaseModel
{
    [MaxLength(256)]
    public string Name {get; set;} = null!;
    public string? Description {get; set;} 
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    public CourseStatus Status {get; set;}
 
    //data annotations or attribute 
    [Column("course_size", TypeName = "smallint")]
    public int Size {get; set;}
    public ICollection<Student> Students {get; set;} = null!;

    public enum CourseStatus
    {
        NotStarted,
        Ongoing,
        Ended
    }
}