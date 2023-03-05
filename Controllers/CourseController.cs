namespace NetCoreCourse.Controllers;

using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

public class CourseController : ApiControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly ICourseService _service;
    private readonly IConfiguration _config;
    private readonly IOptions<CourseSettings> _settings;

    public CourseController(ICourseService service, 
                            IOptions<CourseSettings> settings,
                            ILogger<CourseController> logger, 
                            IConfiguration config)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service;
        _config = config;
        _settings = settings;
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
    public ActionResult<Course?> Create(CourseDTO request)
    {
        // var minSize = _config.GetValue<int>("Course:Size:Min");
        // var maxSize = _config.GetValue<int>("Course:Size:Max");
        // if(request.CourseSize < minSize || request.CourseSize > maxSize)
        // {
        //     return BadRequest("Wrong group size.");
        // }
        if(request.CourseSize > _settings.Value.MaxSize || request.CourseSize < _settings.Value.MinSize)
        {
            return BadRequest("Wrong group size.");
        }
        var course = _service.Create(request);
        if(course is null)
        {
            return BadRequest();
        }
        return course;
    }

    //Update by Id
    [HttpPut("{id:int}")]
    public ActionResult<Course?> Update(int id, CourseDTO request)
    {
        var course = _service.Update(id, request);
        if(course is null)
        {
            return NotFound("Course is not found.");
        }
        return course;
    }

    //Delete by Id
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if(_service.Delete(id)) 
        {
            return Ok(new {Message = "Course is deleted."});
        }
        return NotFound("Course is not found.");
    }
}