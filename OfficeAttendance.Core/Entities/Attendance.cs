namespace OfficeAttendance.Core.Entities;

public class Attendance {
    public int Id { get; init; }
    public required int EmployeeId { get; init; }
    public required DateOnly Date { get; init; }
}
