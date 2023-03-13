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

    public async Task<ICollection<Course>> GetCoursesByStatusAsync(FilterDTO filter)
    {
        // var query = _dbContext.Courses.Where(c => true);
        var query = _dbContext.Courses.AsQueryable();
        if(filter.Status is not null)
        {
            query = query.Where(c => c.Status == filter.Status);
        }
        if(!string.IsNullOrEmpty(filter.Search))
        {
            query = query.Where(c => c.Name.Contains(filter.Search));
        }
        if(filter.StartDate is not null)
        {
            query = query.Where(c => c.StartDate >= filter.StartDate);
        }
        if(filter.EndDate is not null)
        {
            query = query.Where(c => c.EndDate <= filter.EndDate);
        }
        return await query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        //  return await _dbContext.Courses
        //                 .Where(c => c.Status == filter.Status)
        //                 .ToListAsync();
    }

    public override async Task<Course?> GetByIdAsync(int id)
    {
        return await _dbContext.Courses
                        .Include(c => c.Students)
                            .ThenInclude(s => s.Address)
                        .FirstOrDefaultAsync(c => c.Id == id);
        // var course = await base.GetByIdAsync(id);
        // await _dbContext.Entry<Course>(course!).Collection(c => c.Students).LoadAsync();
        // return course;
    }

    public override async Task<ICollection<Course>> GetAllAsync(int page = 0, int pageSize = 9)
    {
        return await _dbContext.Courses
                        .Include(c => c.Students)
                            .ThenInclude(s => s.Address)
                        .OrderBy(c => c.Id)
                        .ToListAsync();
    }
}