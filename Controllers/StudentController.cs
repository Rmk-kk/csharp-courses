namespace NetCoreCourse.Controllers;

using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;
using Microsoft.AspNetCore.Mvc;

public class StudentController : ApiControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly ICrudService<Student, StudentDTO> _service;
    private readonly IConfiguration _config;
    // private readonly IOptions<CourseSettings> _settings;

    public StudentController(ICrudService<Student, StudentDTO> service, 
                            ILogger<StudentController> logger, 
                            IConfiguration config)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _service = service;
        _config = config;
        // _settings = settings;
    }

    //Get all
    [HttpGet]
    public ICollection<Student> GetAll()
    {
        return _service.GetAll();
    }

    //Get by Id
    [HttpGet("{id:int}")] 
    public ActionResult<Student?> Get(int id)
    {
       var student =  _service.Get(id);
       if(student is null)
       {
            return NotFound("Student not found.");
       }
       return student;
    }

    //Create new
    [HttpPost]
    public ActionResult<Student?> Create(StudentDTO request)
    {
        // var minSize = _config.GetValue<int>("Course:Size:Min");
        // var maxSize = _config.GetValue<inStudentt>("Course:Size:Max");
        // if(request.CourseSize < minSize || request.CourseSize > maxSize)
        // {
        //     return BadRequest("Wrong group size.");
        // }
        // if(request.CourseSize > _settings.Value.MaxSize || request.CourseSize < _settings.Value.MinSize)
        // {
        //     return BadRequest("Wrong group size.");
        // }
        var student = _service.Create(request);
        if(student is null)
        {
            return BadRequest();
        }
        return student;
    }

    //Update by Id
    [HttpPut("{id:int}")]
    public ActionResult<Student?> Update(int id, StudentDTO request)
    {
        var course = _service.Update(id, request);
        if(course is null)
        {
            return NotFound("Student is not found.");
        }
        return course;
    }

    //Delete by Id
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if(_service.Delete(id)) 
        {
            return Ok(new {Message = "Student is deleted."});
        }
        return NotFound("Student is not found.");
    }
}