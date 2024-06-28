using Microsoft.EntityFrameworkCore;
using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(u => u.Id);
			modelBuilder.Entity<Attendance>().HasKey(a => a.Id);
			modelBuilder.Entity<Attendance>()
				.HasOne<User>()
				.WithMany()
				.HasForeignKey(a => a.UserId);
		}
    }
}