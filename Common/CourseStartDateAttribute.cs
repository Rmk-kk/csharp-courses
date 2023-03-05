using System.ComponentModel.DataAnnotations;
namespace NetCoreCourse.Common;

public class CourseStartDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if(value is null)
        {
            return false;
        }
        var startDate = (DateTime)value;
        return startDate.Year == DateTime.Now.Year;
    }
}