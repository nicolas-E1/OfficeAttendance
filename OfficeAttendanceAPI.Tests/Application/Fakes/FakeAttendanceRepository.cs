using OfficeAttendanceAPI.Core.Entities;
using OfficeAttendanceAPI.Core.Interfaces;

namespace OfficeAttendanceAPI.Tests.Fakes
{
    public class FakeAttendanceRepository : IAttendanceRepository
    {
        private List<Employee> _attendance = new();
        public bool WasGetByWeekCalled { get; private set; } = false;
        
        public void SetAttendance(IEnumerable<Employee> attendance)
        {
            _attendance = new List<Employee>(attendance);
        }

        public Task<IEnumerable<Employee>> GetByDay(DateOnly date, CancellationToken ct) => throw new NotImplementedException();

        public Task<IEnumerable<Employee>> GetByWeek(CancellationToken cancellationToken)
        {
            WasGetByWeekCalled = true;
            return Task.FromResult<IEnumerable<Employee>>(_attendance);
        }

        public Task<IEnumerable<Attendance>> GetByUserId(int id, CancellationToken ct) => throw new NotImplementedException();
        public Task<Attendance> ConfirmAttendance(Attendance attendance, CancellationToken ct) => throw new NotImplementedException();
        public Task<Attendance> DeleteAttendance(int id, CancellationToken ct) => throw new NotImplementedException();
    }
}
