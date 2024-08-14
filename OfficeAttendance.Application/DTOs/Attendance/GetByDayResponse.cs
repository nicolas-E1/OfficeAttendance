using OfficeAttendance.Core.Entities;

namespace OfficeAttendance.Application.DTOs.Attendance;

public class GetByDayResponse {
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}
