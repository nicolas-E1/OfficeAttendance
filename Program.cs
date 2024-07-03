using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OfficeAttendanceAPI.Application.Interfaces;
using OfficeAttendanceAPI.Infrastructure.Data;
using OfficeAttendanceAPI.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("OfficeAttendanceDb");
});
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddControllers();
builder.Services.AddFastEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFastEndpoints();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();