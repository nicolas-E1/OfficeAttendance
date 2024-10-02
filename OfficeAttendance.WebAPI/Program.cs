using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using OfficeAttendance.Application.UseCases.Attendance;
using OfficeAttendance.Core.Interfaces;
using OfficeAttendance.Infrastructure;
using OfficeAttendance.Infrastructure.Data.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

const string OriginsWhitelist = "_originsWhitelist";

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

if (allowedOrigins == null || allowedOrigins.Length == 0) {
    throw new InvalidOperationException("CORS allowed origins not configured properly. Please check the 'Cors:AllowedOrigins' setting in the configuration files.");
}

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddScoped<GetAttendanceByDayUseCase>();
builder.Services.AddScoped<GetAttendanceByWeekUseCase>();
builder.Services.AddScoped<SetAttendanceForEmployeeUseCase>();

builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<AppDbContext>(options => {
    _ = options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.SwaggerDocument(options => {
    options.DocumentSettings = settings => {
        settings.Title = "Office Attendance API";
        settings.Version = "v1";
        settings.Description = "API to help you manage your office attendance. It allows you to manage employees and their attendance. It also allows you to know who's going to the office each day.";
    };
});

builder.Services.AddCors(options => {
    options.AddPolicy(name: OriginsWhitelist,
        builder => {
            _ = builder.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

WebApplication app = builder.Build();

app.UseCors(OriginsWhitelist);
app.UseFastEndpoints().UseSwaggerGen();

app.Run();
