using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

namespace NetCoreCourse.Services
{
    public interface IProjectService : ICrudService<Project, ProjectDTO>
    {
       Task<int> AddStudentsAsync(int id, ICollection<ProjectAddStudentDTO> students);
       Task<ICollection<Student>> GetStudentsFromCourse(int id);
    }
}