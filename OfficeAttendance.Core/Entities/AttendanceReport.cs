namespace OfficeAttendance.Core.Entities;

public class AttendanceReport {
    public DateOnly Date { get; set; }
    public IEnumerable<Employee> Employees { get; set; } = [];
}
