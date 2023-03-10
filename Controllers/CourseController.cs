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
    
    // [HttpGet("status")]
    // public async Task<ICollection<Course>> GetCoursesByStatusAsync([FromQuery] Course.CourseStatus status)
    // {
        
    // }

    // [HttpGet]
    // public async override Task<ICollection<Course>> GetAllAsync()
    // {
    //     var queryStatus = HttpContext.Request.Query["status"];
    //     if(Enum.TryParse(queryStatus, true, out Course.CourseStatus status))
    //     {
    //         return await _service.GetCoursesByStatusAsync(status);
    //     }
    //     return await _service.GetAllAsync();
    // }

    [HttpGet("status")]
    public async Task<ICollection<Course>> GetCourseByStatus([FromQuery] FilterDTO param)
    {
        Console.WriteLine(param);
        return await _service.GetCoursesByStatusAsync(param);
    }
}