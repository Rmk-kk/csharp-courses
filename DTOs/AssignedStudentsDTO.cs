namespace NetCoreCourse.DTOs;

using NetCoreCourse.Models;

public class AssignedStudentsDTO 
{
    public ICollection<int> Students {get; set;} = null!;
}