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
                        .OrderBy(s => s.Id)
                        .ToListAsync();
    }
}