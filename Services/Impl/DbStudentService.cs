using NetCoreCourse.Db;
using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;

namespace NetCoreCourse.Services;

public class DbStudentService : DbCrudService<Student, StudentDTO>
{
    public DbStudentService(AppDbContext dbContext) : base(dbContext)
    {
    }
}