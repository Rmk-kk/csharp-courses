namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public interface ICourseService : ICrudService<Course, CourseDTO>
{
}