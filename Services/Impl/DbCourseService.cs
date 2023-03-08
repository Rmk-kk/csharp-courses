namespace NetCoreCourse.Services;

using NetCoreCourse.Db;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using Microsoft.EntityFrameworkCore;

public class DbCourseService : DbCrudService<Course, CourseDTO>, ICourseService
{
    public DbCourseService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Course>> GetCoursesByStatusAsync(Course.CourseStatus status)
    {
       return await _dbContext.Courses
                        .Where(c => c.Status == status)
                        .ToListAsync();
    }
}