using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;

namespace NetCoreCourse.Controllers
{
    public class ProjectController : CrudController<Project, ProjectDTO>
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectService _projectService;
        public ProjectController(ILogger<ProjectController> logger, IProjectService service) : base(service)
        {
            _logger = logger;
            _projectService = service;
        }

        [HttpPost("{id}/add-students")]
        public async Task<IActionResult> AddStudents(int id, [FromBody] ICollection<ProjectAddStudentDTO> students)
        {
            var added = await _projectService.AddStudentsAsync(id, students);
            if(added <= 0)
            {
                return BadRequest("No valid student Id");
            } 
            else 
            {
                // return Ok(new {Count = added});
                return Ok($"{added} students were added to project {id}");
            }
        }

        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudentsFromCourse(int id)
        {
            var students = await _projectService.GetStudentsFromCourse(id);
            if(students is null)
            {
                return BadRequest();
            } 
            else 
            {
                return Ok(students);
            }
        }
    }
}