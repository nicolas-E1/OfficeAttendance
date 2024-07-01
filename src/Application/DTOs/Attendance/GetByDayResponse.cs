using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Application.DTOs.Attendance
{
    public class GetAttendanceByDayResponse
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    }
}