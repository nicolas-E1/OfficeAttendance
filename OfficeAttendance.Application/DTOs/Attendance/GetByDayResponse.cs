using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Application.DTOs.Attendance;

public class GetByDayResponse
{
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}
