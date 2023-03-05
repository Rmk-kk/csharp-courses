using System.ComponentModel.DataAnnotations;
namespace NetCoreCourse.Common;

public class CourseSizeAttribute : ValidationAttribute
{
    private readonly IConfiguration _config;
    public CourseSizeAttribute(IConfiguration config) => _config = config;
    public override bool IsValid(object? value)
    {
        if(value is null)
        {
            return false;
        }
        var size = (int)value;
        var minSize = _config.GetValue<int>("Course:Size:Min");
        var maxSize = _config.GetValue<int>("Course:Size:Max");
        return size >= minSize && size <= maxSize;
    }
}