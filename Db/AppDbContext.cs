namespace NetCoreCourse.Db;

using Microsoft.EntityFrameworkCore;
using NetCoreCourse.Models;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    public AppDbContext(IConfiguration config) => _config = config;
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString)
                      .UseSnakeCaseNamingConvention();
    }       

    public DbSet<Course> Courses {get; set;}
}