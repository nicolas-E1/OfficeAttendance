using OfficeAttendance.Core.Entities;

namespace OfficeAttendance.Application.DTOs.Attendance;

public class GetByWeekResponse {
    public IEnumerable<AttendanceReport> AttendanceReport { get; set; } = [];
}
