using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using OfficeAttendanceAPI.Application.Interfaces;
using OfficeAttendanceAPI.Infrastructure.Data;
using OfficeAttendanceAPI.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("OfficeAttendanceDb");
});
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "Office Attendance API";
        s.Version = "v1";
        s.Description = "API for tracking office attendance";
    };
});
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

var app = builder.Build();

app.UseFastEndpoints().UseSwaggerGen();

app.Run();