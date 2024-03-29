using NetCoreCourse.Services;
using NetCoreCourse.Models;
using System.Text.Json.Serialization;
using NetCoreCourse.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services
    .AddAuthentication(option => 
    {   
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    //How to validate token  
    .AddJwtBearer(options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("FS13", policy =>
    {
        policy.RequireClaim("Course", "FS13");
    });

    options.AddPolicy("PremiumSubscriber", policy =>
    {
        policy.RequireClaim("subscriptionLevel", "Premium");
    });

    options.AddPolicy("MeOnly", policy =>
    {
        policy.RequireUserName("daniel.monero@gmail.com");
    });
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin"); // === [Authorsize(Roles = "Admin")]
    });

    // Allow access only if:
    // - Has admin role
    // - Age > 18
    // - Never miss a lecture
    // - Frontend score: > 4

    options.AddPolicy("Custom", policy =>
    {
        policy.RequireAssertion(handler =>
        {
            // Do something to determine if user has access
            // Get back the user object and do the checks
            return true;
        });
    });
});

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
