
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebApplication1.Configs;
using WebApplication1.Data;
using WebApplication1.Repository;
using WebApplication1.Service.AuthService;
using WebApplication1.Service.DepartmentService;
using WebApplication1.Service.StaffService;
using IStaffService = WebApplication1.Service.StaffService.IStaffService;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

// Add JWT configuration

// Add this after the JWT settings configuration
// JWT Configuration trong Program.cs
builder.Services.AddScoped<IJwtService, JwtService>();

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure ClassLibrary1.DAL DbConnection With Dapper
builder.Services.AddScoped<ClassLibrary1.DAL.DbConnection.DbConnection>(provider =>
{
    return new ClassLibrary1.DAL.DbConnection.DbConnection(connectionString);
});

// Configure SqlQueryLoader DAL 
builder.Services.AddScoped<ClassLibrary1.DAL.DbConnection.SqlQueryLoader>();

// Register DAL Services DAL
builder.Services.AddScoped<ClassLibrary1.DAL.DALService.IStaffService, WorkerService1.DAL.DALService.StaffService>();
builder.Services.AddScoped<ClassLibrary1.DAL.DALDepartmentService.IDepartmentService, ClassLibrary1.DAL.DALDepartmentService.DepartmentService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerEventsHandler.GetJwtBearerOptions(builder.Configuration));

// Thêm Authorization (bắt buộc nếu dùng [Authorize])
builder.Services.AddAuthorization();
//configure AppDbContext
builder.Services.AddControllers(options =>
{
    // Đăng ký filter cho toàn bộ ứng dụng
    options.Filters.Add<CustomExceptionFilter>();
});

// Configure MySQL DbContext With Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
    ));
//Register Repositories 
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<StaffRepository>();
builder.Services.AddScoped<AuthRepository>();


// Register services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add controllers
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BaseApi",
        Version = "v1",
        Description = "API for managing staff and departments"
    });

    // Cấu hình để sử dụng JWT Bearer Authentication trong Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = "Please enter 'Bearer' [space] and then your token"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


var app = builder.Build();

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Root endpoint hit at {time}", DateTime.UtcNow);
    return "Hello, world!";
});

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseApi v1"); });
}
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }