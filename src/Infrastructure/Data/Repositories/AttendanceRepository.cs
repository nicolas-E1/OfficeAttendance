using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OfficeAttendanceAPI.Application.Interfaces;
using OfficeAttendanceAPI.Core.Entities;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.Infrastructure.Data.Repositories
{
    public class AttendanceRepository(AppDbContext dbContext) : IAttendanceRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct)
        {
            try 
            {
                var userIds = await _dbContext.Attendances
                    .Where(a => a.Date == date)
                    .Select(a => a.EmployeeId)
                    .ToListAsync(ct);

                return await _dbContext.Employees
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                throw new AttendanceNotFoundException("Failed to get attendance by day", ex);
            }
        }

        public async Task<IEnumerable<Employee>> GetByWeek(CancellationToken ct)
        {
            try
            {
                var today = DateTime.Today;
                var startOfWeek = today.AddDays(-((int)today.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
                var endOfWeek = startOfWeek.AddDays(7);
                var userIds =  await _dbContext.Attendances
                    .Where(a => a.Date.ToDateTime(TimeOnly.FromDateTime(today)) >= startOfWeek && a.Date.ToDateTime(TimeOnly.FromDateTime(today)) < endOfWeek)
                    .Select(a => a.EmployeeId)
                    .Distinct()
                    .ToListAsync(ct);

                return await _dbContext.Employees
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                throw new AttendanceNotFoundException("Failed to get attendance by week", ex);
            }
        }

        public async Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct)
        {
            try
            {
                return await _dbContext.Attendances
                    .Where(a => a.EmployeeId == id)
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                throw new AttendanceNotFoundException("Failed to get attendance by user id", ex);
            }
        }

        public async Task<Attendance> ConfirmAttendance(Attendance attendance, CancellationToken ct)
        {
            try
            {
                var existingAttendance = await _dbContext.Attendances
                    .Where(a => a.EmployeeId == attendance.EmployeeId && a.Date == attendance.Date)
                    .FirstOrDefaultAsync(ct);

                if (existingAttendance != null) return existingAttendance;

                _dbContext.Attendances.Add(attendance);
                await _dbContext.SaveChangesAsync(ct);

                return attendance;
            }
            catch (Exception ex)
            {
                throw new AttendanceConfirmException("Failed to confirm attendance", ex);
            }
        }

        public async Task<Attendance> DeleteAttendance(int id, CancellationToken ct)
        {
            try
            {
                var attendance = 
                    await _dbContext.Attendances.FindAsync(id) ?? 
                    throw new AttendanceNotFoundException($"Failed to cancel attendance with id: {id}. Attendance not found.");

                _dbContext.Attendances.Remove(attendance);
                await _dbContext.SaveChangesAsync(ct);

                return attendance;
            }
            catch (Exception ex)
            {
                throw new AttendanceDeletionException("Failed to cancel attendance", ex);
            }
        }
    }
}