namespace NetCoreCourse.Db;

using Microsoft.EntityFrameworkCore;
using NetCoreCourse.Models;
using System.Diagnostics;
using Npgsql;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    private readonly ILogger<AppDbContext> _logger;
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
                      .AddInterceptors(new AppDbContextSaveChangesInterceptor());
    }       

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        //map enum 
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();
        //add indexing
        modelBuilder.Entity<Course>()
            .HasIndex(course => course.Name);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Student>()
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Address>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Address>()
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Address)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull);                                              

        //include whole address when returning student from DB
        //First option: (second option in student service)
        // modelBuilder.Entity<Student>()
        //     .Navigation(s => s.Address)
        //     .AutoInclude();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses {get; set;} = null!;
    public DbSet<Student> Students {get; set;} = null!;
    public DbSet<Address> Addresses {get; set;} = null!;
    public DbSet<Assigment> Assigments {get; set;} = null!;
}