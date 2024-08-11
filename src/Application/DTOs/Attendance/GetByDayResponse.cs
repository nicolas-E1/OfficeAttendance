using OfficeAttendanceAPI.src.Core.Entities;

namespace OfficeAttendanceAPI.src.Application.DTOs.Attendance;

public class GetByDayResponse
{
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}