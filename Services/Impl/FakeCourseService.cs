using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;

namespace NetCoreCourse.Services;

public class FakeCourseService : FakeCrudService<Course, CourseDTO>, ICourseService
{ 
    public async Task<ICollection<Course>> GetCoursesByStatusAsync(Course.CourseStatus status)
    {  
        return await Task.Run(() => {
            return _items.Values
                .Where(course => course.Status == status)
                .ToList();
        }); 
    }
}