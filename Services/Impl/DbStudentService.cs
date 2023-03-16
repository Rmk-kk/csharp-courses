using NetCoreCourse.Db;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace NetCoreCourse.Services;

public class DbStudentService : DbCrudService<Student, StudentDTO>, IStudentService
{
    public DbStudentService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async override Task<ICollection<Student>> GetAllAsync(int page = 0, int pageSize = 9)
    {
        return await _dbContext.Students
                        .AsNoTracking()
                        .Include(s => s.Address)
                        .Include(s => s.Course)
                        .Include(s => s.Assigments)
                        .Include(s => s.ProjectLinks)
                            .ThenInclude(pl => pl.Project)
                        .OrderBy(s => s.Id)
                        .ToListAsync();
    }

    public async override Task<Student?> GetByIdAsync(int id)
    {
        //eager loading vs lazy loading  (remove include for lazy)

        var student = await base.GetByIdAsync(id);
        return await _dbContext.Students
                        .Include(s => s.Address)
                        .Include(s => s.Assigments)
                        .FirstOrDefaultAsync(s => s.Id == id);
    }
}