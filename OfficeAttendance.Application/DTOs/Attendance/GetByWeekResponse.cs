using OfficeAttendance.Core.Entities;

namespace OfficeAttendance.Application.DTOs.Attendance;

public class GetByWeekResponse {
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}
