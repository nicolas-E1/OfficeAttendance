using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Interfaces;
using OfficeAttendance.Core.Exceptions.Attendance;

namespace OfficeAttendance.Infrastructure.Data.Repositories;

public class AttendanceRepository(AppDbContext dbContext) : IAttendanceRepository {
    public async Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct) {
        try {
            var userIds = await dbContext.Attendances
                .Where(a => a.Date == date)
                .Select(a => a.EmployeeId)
                .ToListAsync(ct);

            return await dbContext.Employees
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync(ct);
        }
        catch (Exception ex) {
            throw new AttendanceNotFoundException("Failed to get attendance by day", ex);
        }
    }

    public async Task<IEnumerable<AttendanceReport>> GetByWeek(CancellationToken ct) {
        try {
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays(-((int)today.DayOfWeek - (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(7);

            List<Attendance> attendanceRecords = await dbContext.Attendances
                .Where(a => a.Date.ToDateTime(TimeOnly.FromDateTime(today)) >= startOfWeek && a.Date.ToDateTime(TimeOnly.FromDateTime(today)) < endOfWeek)
                .ToListAsync(ct);

            IEnumerable<int> employeeIds = attendanceRecords.Select(a => a.EmployeeId).Distinct();

            List<Employee> employees = await dbContext.Employees
                .Where(e => employeeIds.Contains(e.Id))
                .ToListAsync(ct);

            var attendanceReport = attendanceRecords
                .GroupBy(a => a.Date)
                .Select(g => new AttendanceReport {
                    Date = g.Key,
                    Employees = g.Select(a => employees.First(e => e.Id == a.EmployeeId)).ToList()
                }).ToList();

            return attendanceReport;
        }
        catch (Exception ex) {
            throw new AttendanceNotFoundException("Failed to get attendance by week", ex);
        }
    }

    public async Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct) {
        try {
            return await dbContext.Attendances
                .Where(a => a.EmployeeId == id)
                .ToListAsync(ct);
        }
        catch (Exception ex) {
            throw new AttendanceNotFoundException("Failed to get attendance by user id", ex);
        }
    }

    public async Task<Attendance> ConfirmAttendance(Attendance attendance, CancellationToken ct) {
        try {
            var existingAttendance = await dbContext.Attendances
                .Where(a => a.EmployeeId == attendance.EmployeeId && a.Date == attendance.Date)
                .FirstOrDefaultAsync(ct);

            if (existingAttendance != null) {
                return existingAttendance;
            }

            dbContext.Attendances.Add(attendance);
            await dbContext.SaveChangesAsync(ct);

            return attendance;
        }
        catch (Exception ex) {
            throw new AttendanceConfirmException("Failed to confirm attendance", ex);
        }
    }

    public async Task<Attendance> DeleteAttendance(int id, CancellationToken ct) {
        try {
            var attendance =
                await dbContext.Attendances.FindAsync(id) ??
                throw new AttendanceNotFoundException($"Failed to cancel attendance with id: {id}. Attendance not found.");

            dbContext.Attendances.Remove(attendance);
            await dbContext.SaveChangesAsync(ct);

            return attendance;
        }
        catch (Exception ex) {
            throw new AttendanceDeletionException("Failed to cancel attendance", ex);
        }
    }
}
