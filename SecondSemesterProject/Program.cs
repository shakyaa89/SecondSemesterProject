using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Application.Interfaces.IRepository;
using SecondSemesterProject.Application.Interfaces.IService;
using SecondSemesterProject.Controllers;
using SecondSemesterProject.Infrastructure.Repositories;
using SecondSemesterProject.Infrastructure.Persistence;
using SecondSemesterProject.Infrastructure.Services;
using SecondSemesterProject.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<MyInfoConfig>(builder.Configuration.GetSection("MyInfo"));

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IModuleInstructorRepository, ModuleInstructorRepository>();
builder.Services.AddScoped<IModuleInstructorService, ModuleInstructorService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddIdentity<Users, Roles>().AddEntityFrameworkStores<ApplicationDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
