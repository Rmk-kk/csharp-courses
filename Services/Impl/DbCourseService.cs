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

    public override async Task<Course?> GetByIdAsync(int id)
    {
        return await _dbContext.Courses
                        .Include(c => c.Students)
                        .FirstOrDefaultAsync(c => c.Id == id);
        // var course = await base.GetByIdAsync(id);
        // await _dbContext.Entry<Course>(course!).Collection(c => c.Students).LoadAsync();
        // return course;
    }
}