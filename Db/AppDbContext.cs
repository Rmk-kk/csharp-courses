namespace NetCoreCourse.Db;

using Microsoft.EntityFrameworkCore;
using NetCoreCourse.Models;
using System.Diagnostics;
using Npgsql;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    private readonly ILogger<AppDbContext> _logger;
    public DbSet<Course> Courses {get; set;} = null!;
    public DbSet<Student> Students {get; set;} = null!;
    public AppDbContext(IConfiguration config, ILogger<AppDbContext> logger, DbContextOptions<AppDbContext> options) : base(options)
    {
        _config = config;
        _logger = logger;
    } 
    //with static created only once
    static AppDbContext() {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _config.GetConnectionString("DefaultConnection");
        
        optionsBuilder.UseNpgsql(connectionString)
                      .UseSnakeCaseNamingConvention()
                      .LogTo(m => Debug.WriteLine(m));
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