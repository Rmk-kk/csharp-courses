namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public interface IAssigmentService : ICrudService<Assigment, AssigmentDTO>
{
    Task<int> AssignStudentsAsync(int id, ICollection<int> studentIds);
    Task <int> RemoveStudentsAsync(int id, ICollection<int> studentId);
}