using OfficeAttendanceAPI.Core.Entities;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;
using OfficeAttendanceAPI.Core.Interfaces;

namespace OfficeAttendanceAPI.Tests.Fakes
{
    public class FakeAttendanceRepository : IAttendanceRepository
    {
        private readonly Dictionary<int, List<Employee>> _attendance = [];
        public bool WasGetByWeekCalled { get; private set; } = false;
        public bool ShouldThrowException { get; set; } = false;
        private int _currentWeek = 1;

        public void SetAttendance(IEnumerable<Employee>? attendance) => _attendance[_currentWeek] =  attendance?.ToList() ?? [];

        public void SetAttendanceForWeek(IEnumerable<Employee> attendance, int week) => _attendance[week] =  attendance?.ToList() ?? [];

        public void SetCurrentWeek(int week) => _currentWeek = week;

        public Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct) => throw new NotImplementedException();

        public Task<IEnumerable<Employee>> GetByWeek(CancellationToken cancellationToken)
        {
            WasGetByWeekCalled = true;
            if (ShouldThrowException) {
                throw new AttendanceNotFoundException("Failed to get attendance by week");
            }
            _attendance.TryGetValue(_currentWeek, out List<Employee>? attendance);
            return Task.FromResult<IEnumerable<Employee>>(attendance ?? []);
        }

        public Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct) => throw new NotImplementedException();
        public Task<Attendance> ConfirmAttendance(Attendance attendance, CancellationToken ct) => throw new NotImplementedException();
        public Task<Attendance> DeleteAttendance(int id, CancellationToken ct) => throw new NotImplementedException();
    }
}
