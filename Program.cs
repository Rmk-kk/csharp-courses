using NetCoreCourse.Services;
using NetCoreCourse.Models;
using System.Text.Json.Serialization;
using NetCoreCourse.Db;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//adding auth
builder.Services
    .AddIdentity<User, IdentityRole<int>>(options => 
    {
        options.Password.RequiredLength = 6;
        //DONT DO IT IN PRODUCTION
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<AppDbContext>();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Database connection
builder.Services.AddDbContext<AppDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//singleton only because not real database
builder.Services.AddScoped<ICourseService, DbCourseService>();
builder.Services.AddScoped<IStudentService, DbStudentService>();
builder.Services.AddScoped<IAssigmentService, DbAssigmentService>();
builder.Services.AddScoped<IProjectService, DbProjectsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, JWTTokenService>();

//configuration file for Course
builder.Services.Configure<CourseSettings>(builder.Configuration.GetSection("Course:Size"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreCourse");
        options.RoutePrefix = string.Empty;
    });

    using(var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        var config = scope.ServiceProvider.GetService<IConfiguration>();

        if(dbContext is not null && config!.GetValue<bool>("CreateDbAtStart", false))
        {
            dbContext!.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}

app.UseHttpsRedirection();

//HAS TO BE BEFORE AUTHORAZATION!!
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
