namespace NetCoreCourse.Controllers;

using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;

public class CourseController : CrudController<Course, CourseDTO>
{
    private readonly ILogger<CourseController> _logger;
    public CourseController(ILogger<CourseController> logger, ICrudService<Course, CourseDTO> service) : base(service)
    {
        _logger = logger;
    }
}