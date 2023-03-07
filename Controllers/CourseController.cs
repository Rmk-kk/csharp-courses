namespace NetCoreCourse.Controllers;

using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;
using Microsoft.AspNetCore.Mvc;

public class CourseController : CrudController<Course, CourseDTO>
{
    private readonly ILogger<CourseController> _logger;
    private readonly ICourseService _service;
    public CourseController(ILogger<CourseController> logger, ICourseService service) : base(service)
    {
        _logger = logger;
        _service = service;
    }

    // [HttpGet("{status}")]
    // public ICollection<Course> GetCoursesByStatus(Course.CourseStatus status)
    // {
    //     return _service.GetCoursesByStatus(status);
    // }
    [HttpGet("status")]
    public async Task<ICollection<Course>> GetCoursesByStatusAsync([FromQuery] Course.CourseStatus status)
    {
        return await _service.GetCoursesByStatusAsync(status);
    }
}