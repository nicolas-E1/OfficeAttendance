using OfficeAttendance.Core.Entities;

namespace OfficeAttendance.Application.DTOs.Attendance;

public class SetAttendanceResponse {
    public required Employee Employee { get; set; }
    public required DateOnly Date { get; set; }
}
