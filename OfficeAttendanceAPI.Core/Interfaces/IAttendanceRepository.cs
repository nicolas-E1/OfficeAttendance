using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Core.Interfaces;

public interface IAttendanceRepository
{
    Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct);
    Task<IEnumerable<Employee>> GetByWeek(CancellationToken ct);
    Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct);
    Task<Attendance> ConfirmAttendance(Attendance attendance, CancellationToken ct);
    Task<Attendance> DeleteAttendance(int id, CancellationToken ct);
}