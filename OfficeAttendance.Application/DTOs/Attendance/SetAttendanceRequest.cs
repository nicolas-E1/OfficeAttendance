namespace OfficeAttendance.Application.DTOs.Attendance;

public class SetAttendanceRequest {
    public int EmployeeId { get; set; }
    public DateOnly Date { get; set; }
}
