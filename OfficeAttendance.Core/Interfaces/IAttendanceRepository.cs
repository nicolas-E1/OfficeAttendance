using OfficeAttendance.Core.Entities;

namespace OfficeAttendance.Core.Interfaces;

public interface IAttendanceRepository {
    Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct);
    Task<IEnumerable<AttendanceReport>> GetByWeek(CancellationToken ct);
    Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct);
    Task<AttendanceReport> ConfirmAttendance(Attendance attendance, CancellationToken ct);
    Task<Attendance> DeleteAttendance(int id, CancellationToken ct);
}
