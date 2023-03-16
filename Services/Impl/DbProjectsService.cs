using NetCoreCourse.Models;
using NetCoreCourse.DTOs;
using NetCoreCourse.Db;
using Microsoft.EntityFrameworkCore;

namespace NetCoreCourse.Services
{
    public class DbProjectsService : DbCrudService<Project, ProjectDTO>, IProjectService
    {
        public DbProjectsService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Student>> GetStudentsFromCourse(int id)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
            if(project is null)
            {
                
            }
            var studentIds = await _dbContext.ProjectStudents.Where(p => p.ProjectId == project.Id).Select(p => p.StudentId).ToListAsync();
            var students = await _dbContext.Students.Where(s => studentIds.Contains(s.Id)).ToListAsync();
            return students;
        }

        public async override Task<Project?> GetByIdAsync(int id)
        {
            return await _dbContext.Projects
                            .Include(p => p.StudentLinks)
                            .SingleOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<int> AddStudentsAsync(int id, ICollection<ProjectAddStudentDTO> request)
        {
            var project = await GetByIdAsync(id);
            if(project is null)
            {
                return -1;
            }
            var studentIds = request
                                .Select(item => item.StudentId)
                                .ToList();
             
            var students = await _dbContext.Students
                                .Where(s => studentIds.Contains(s.Id))
                                .ToListAsync();

            // foreach(var student in students)
            // {
            //         project.StudentLinks.Add(new ProjectStudent {
            //         Project = project,
            //         JoinedAt = DateTime.Now,
            //         Role = request.FirstOrDefault(s => s);
            //     }); 
            // }

            foreach(var studentDto in request)
            {
                project.StudentLinks.Add(new ProjectStudent 
                {
                    Project = project,
                    JoinedAt = DateTime.Now,
                    Role = studentDto.Role,
                    StudentId = studentDto.StudentId,
                }); 
            }
            await _dbContext.SaveChangesAsync();
            return students.Count();
        }
    }
}