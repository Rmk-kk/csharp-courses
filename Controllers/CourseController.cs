namespace NetCoreCourse.Controllers;

using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;
using Microsoft.AspNetCore.Mvc;

public class CourseController : ApiControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly ICourseService _service;
    public CourseController(ICourseService service, ILogger<CourseController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service;
    }

    //Get all
    [HttpGet]
    public ICollection<Course> GetAll()
    {
        return _service.GetAll();
    }

    //Get by Id
    [HttpGet("{id:int}")] 
    public ActionResult<Course?> Get(int id)
    {
       var course =  _service.Get(id);
       if(course is null)
       {
            return NotFound("Course not found.");
       }
       return course;
    }

    //Create new
    [HttpPost]
    public IActionResult Create(CourseDTO request)
    {
        return Ok(_service.Create(request));
    }

    //Update by Id
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CourseDTO request)
    {
        var course = _service.Update(id, request);
        if(course is null)
        {
            return NotFound("Course is not found.");
        }
        else 
        {
            return Ok(course);
        }
    }

    //Delete by Id
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if(_service.Delete(id)) 
        {
            return Ok(new {Message = "Course is deleted."});
        }
        return NotFound("Course is not found.");
    }
}