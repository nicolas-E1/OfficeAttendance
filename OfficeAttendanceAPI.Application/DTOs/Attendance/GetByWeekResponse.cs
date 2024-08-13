using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Application.DTOs.Attendance;

public class GetByWeekResponse
{
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}
