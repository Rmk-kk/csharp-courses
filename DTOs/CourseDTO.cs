namespace NetCoreCourse.DTOs;

using System.ComponentModel.DataAnnotations;
using NetCoreCourse.Models;
using NetCoreCourse.Common;
using System.Collections.Generic;

public class CourseDTO : BaseDTO<Course> 
{
    [MinLength(5, ErrorMessage = "Name is too short, 5 chars minimum")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "max 200, min 5.")]
    public string Name {get; set;}

    [CourseStartDate(ErrorMessage = "Start Date should be same year.")]
    public DateTime StartDate {get; set;}
    public int CourseSize {get; set;}
    public Course.CourseStatus Status {get; set;}

      public override void UpdateModel(Course model)
    {
        Name = model.Name;
        CourseSize = model.CourseSize;
        StartDate = model.StartDate;
        Status = model.Status;
    }
}