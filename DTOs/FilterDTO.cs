namespace NetCoreCourse.DTOs;

using NetCoreCourse.Models;

public class FilterDTO
{
    public Course.CourseStatus? Status {get; set;}
    public int CurrentPage {get; set;} = 1;
    public int PageSize {get; set;} = 10;
    public DateTime? StartDate {get; set;}
    public DateTime? EndDate {get; set;}
    public string? Search {get; set;} 
}