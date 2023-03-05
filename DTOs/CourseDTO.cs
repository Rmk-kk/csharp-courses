namespace NetCoreCourse.DTOs;

using NetCoreCourse.Models;

public class CourseDTO
{
    public string Name {get; set;}
    public DateTime StartDate {get; set;}
    public Course.CourseStatus Status {get; set;}
}