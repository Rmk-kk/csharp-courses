using NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;
using NetCoreCourse.Db;
using Microsoft.EntityFrameworkCore;

public class DbAssigmentService : DbCrudService<Assigment, AssigmentDTO>, IAssigmentService
{
    public DbAssigmentService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> AssignStudentsAsync(int id, ICollection<int> studentIds)
    {
        var assigment = await GetByIdAsync(id);
        if(assigment is null)
        {
            return -1;
        }
        var students = await _dbContext.Students
                    .Include(s => s.Assigments)
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();

        foreach(var student in students)
        {
            if(!student.Assigments.Contains(assigment))
            {
                student.Assigments.Add(assigment);
            }
        }
        await _dbContext.SaveChangesAsync();
        return students.Count();
    }

    public override async Task<ICollection<Assigment>> GetAllAsync(int page = 0, int pageSize = 9)
    {   
        return await _dbContext.Assigments
                                .Include(c => c.Students)
                                .OrderBy(c => c.Id)
                                .ToListAsync();
    }

    public async Task<int> RemoveStudentsAsync(int id, ICollection<int> studentIds)
    {
        var assigment = await GetByIdAsync(id);
        if(assigment is null)
        {
            return -1;
        }
        var students = await _dbContext.Students
                    .Include(s => s.Assigments)
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();

        foreach(var student in students)
        {
            if(student.Assigments.Contains(assigment))
            {
                student.Assigments.Remove(assigment);
            }
        }
        await _dbContext.SaveChangesAsync();
        return students.Count();
    }

    public override async Task<Assigment?> GetByIdAsync(int id)
    {
       return await _dbContext.Assigments
                        .Include(c => c.Students)
                        .FirstOrDefaultAsync(c => c.Id == id);
    }
}