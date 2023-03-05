namespace NetCoreCourse.DTOs;

using System.ComponentModel.DataAnnotations;
using NetCoreCourse.Models;
using NetCoreCourse.Common;
using System.Collections.Generic;

public class CourseDTO : IValidatableObject
{
    // [MinLength(5, ErrorMessage = "Name is too short, 5 chars minimum")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "max 200, min 5.")]
    public string Name {get; set;}

    [CourseStartDate(ErrorMessage = "Start Date should be same year.")]
    public DateTime StartDate {get; set;}

    public Course.CourseStatus Status {get; set;}

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(StartDate < DateTime.Now && Status == Course.CourseStatus.NotStarted)
        {
            yield return new ValidationResult("Wrong Status. Course should've start already.");
        }
    }
}