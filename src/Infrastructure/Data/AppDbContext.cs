using Microsoft.EntityFrameworkCore;
using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : DbContext(options)
{

    public DbSet<Employee> Employees { get; init; }
    public DbSet<Attendance> Attendances { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Employee>().HasKey(e => e.Id);
		modelBuilder.Entity<Attendance>().HasKey(a => a.Id);
		modelBuilder.Entity<Attendance>()
			.HasOne<Employee>()
			.WithMany()
			.HasForeignKey(a => a.EmployeeId);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
		}
	}
}