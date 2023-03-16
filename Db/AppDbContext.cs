namespace NetCoreCourse.Db;

using Microsoft.EntityFrameworkCore;
using NetCoreCourse.Models;
using Npgsql;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// IdentityUserContext<IdentityUser>
public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    private readonly IConfiguration _config;
    private readonly ILogger<AppDbContext> _logger;
    public AppDbContext(IConfiguration config, ILogger<AppDbContext> logger, DbContextOptions<AppDbContext> options) : base(options)
    {
        _config = config;
        _logger = logger;
    }

    //with static created only once
    [Obsolete]
    static AppDbContext() {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ProjectRole>();
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
        base.OnModelCreating(modelBuilder);
        
        //map enum 
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();
        modelBuilder.HasPostgresEnum<ProjectRole>();
        //add indexing
        modelBuilder.Entity<Course>()
            .HasIndex(course => course.Name);

        //tell EF to use composite primary key 
        modelBuilder.Entity<ProjectStudent>()
            .HasKey(ps => new {ps.StudentId, ps.ProjectId});

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
        
        modelBuilder.AddIdentityConfig();
    }

    public DbSet<Course> Courses {get; set;} = null!;
    public DbSet<Student> Students {get; set;} = null!;
    public DbSet<Address> Addresses {get; set;} = null!;
    public DbSet<Assigment> Assigments {get; set;} = null!;
    public DbSet<Project> Projects {get; set;} = null!;
    public DbSet<ProjectStudent> ProjectStudents {get; set;} = null!;
}