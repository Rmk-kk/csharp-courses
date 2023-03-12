namespace NetCoreCourse.Controllers;

using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;

public class StudentController : CrudController<Student, StudentDTO>
{
    private readonly ILogger<StudentController> _logger;
    public StudentController(ILogger<StudentController> logger, IStudentService service) : base(service)
    {
        _logger = logger;
    }
}