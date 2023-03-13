namespace NetCoreCourse.Controllers;

using Microsoft.AspNetCore.Mvc;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using NetCoreCourse.Services;

public class AssigmentController : CrudController<Assigment, AssigmentDTO>
{
    private readonly ILogger<AssigmentController> _logger;
    private readonly IAssigmentService _assigmentService;
    public AssigmentController(ILogger<AssigmentController> logger, IAssigmentService service) : base(service)
    {
        _logger = logger;
        _assigmentService = service;
    }

    [HttpPost("{id}/assign")]
    public async Task<IActionResult> AssignedStudents(int id, [FromBody] AssignedStudentsDTO request)
    {
        var assigned = await _assigmentService.AssignStudentsAsync(id, request.Students);
        if(assigned <= 0)
        {
            return BadRequest("Wrong students Ids.");
        }
        return Ok(new { Message  = $"{assigned} students have been assigned.", Count = assigned });
    }

    [HttpPost("{id}/unassign")]
    public async Task<IActionResult> RemoveStudents(int id, [FromBody] AssignedStudentsDTO request)
    {
        var assigned = await _assigmentService.RemoveStudentsAsync(id, request.Students);
        if(assigned <= 0)
        {
            return BadRequest("Wrong students Ids.");
        }
        return Ok(new { Message  = $"{assigned} students have been removed.", Count = assigned });
    }
}