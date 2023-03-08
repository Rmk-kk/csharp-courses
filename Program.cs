using NetCoreCourse.Services;
using NetCoreCourse.Models;
using NetCoreCourse.DTOs;
using System.Text.Json.Serialization;
using NetCoreCourse.Db;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

//Database connection

builder.Services.AddDbContext<AppDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//singleton only because not real database
builder.Services.AddScoped<ICourseService, DbCourseService>();
builder.Services.AddScoped<ICrudService<Student, StudentDTO>, DbStudentService>();

//configuration file for Course
builder.Services.Configure<CourseSettings>(builder.Configuration.GetSection("Course:Size"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using(var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        dbContext!.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
