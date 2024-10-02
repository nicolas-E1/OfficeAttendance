using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Exceptions.Attendance;
using OfficeAttendance.Core.Interfaces;

namespace OfficeAttendance.Tests.Application.Fakes;
public class FakeAttendanceRepository : IAttendanceRepository {
    private const int defaultWeek = 1;
    private readonly Dictionary<object, List<Employee>> _attendance = [];
    private readonly Dictionary<int, List<AttendanceReport>> _attendanceReport = [];
    public bool WasGetByWeekCalled { get; private set; } = false;
    public bool WasGetByDayCalled { get; private set; } = false;
    public bool ShouldThrowException { get; set; } = false;
    private int _currentWeek = defaultWeek;

    public void SetAttendance(IEnumerable<Employee>? attendance, int key = defaultWeek) => _attendance[key] = attendance?.ToList() ?? [];
    public void SetAttendance(IEnumerable<Employee>? attendance, DateOnly key) => _attendance[key] = attendance?.ToList() ?? [];
    public void SetAttendanceForWeek(IEnumerable<AttendanceReport> attendance, int week) => _attendanceReport[week] = attendance.ToList() ?? [];
    public void SetCurrentWeek(int week) => _currentWeek = week;

    public Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct) {
        WasGetByDayCalled = true;
        if (ShouldThrowException) {
            throw new AttendanceNotFoundException("Failed to get attendance by day");
        }
        _attendance.TryGetValue(date, out List<Employee>? attendance);
        return Task.FromResult<IEnumerable<Employee>>(attendance ?? []);
    }

    public Task<IEnumerable<AttendanceReport>> GetByWeek(CancellationToken cancellationToken) {
        WasGetByWeekCalled = true;
        if (ShouldThrowException) {
            throw new AttendanceNotFoundException("Failed to get attendance by week");
        }
        _ = _attendanceReport.TryGetValue(_currentWeek, out List<AttendanceReport>? attendance);
        return Task.FromResult<IEnumerable<AttendanceReport>>(attendance ?? []);
    }

    public Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct) => throw new NotImplementedException();
    public Task<AttendanceReport> ConfirmAttendance(Attendance attendance, CancellationToken ct) {
        if (ShouldThrowException) {
            throw new SetAttendanceException("Failed to set attendance for employee");
        }
        return Task.FromResult(new AttendanceReport {
            Date = attendance.Date,
            Employees = [new Employee { Id = attendance.EmployeeId, FirstName = "Dante", LastName = "Alighieri" }]
        });
    }
    public Task<Attendance> DeleteAttendance(int id, CancellationToken ct) => throw new NotImplementedException();
}
