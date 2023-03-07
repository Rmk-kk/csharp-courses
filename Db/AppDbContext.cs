namespace NetCoreCourse.Db;

using Microsoft.EntityFrameworkCore;
using NetCoreCourse.Models;
using Npgsql;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    public DbSet<Course> Courses {get; set;} = null!;
    public DbSet<Student> Students {get; set;} = null!;
    public AppDbContext(IConfiguration config) => _config = config;
    //with static created only once
    static AppDbContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>();
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString)
                      .UseSnakeCaseNamingConvention();
    }       

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        //map enum 
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();
        //add indexing
        modelBuilder.Entity<Course>()
            .HasIndex(course => course.Name);
        base.OnModelCreating(modelBuilder);
    }
}