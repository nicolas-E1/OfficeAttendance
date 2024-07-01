namespace OfficeAttendanceAPI.Core.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public required int EmployeeId { get; set; }
        public required DateOnly Date { get; set; }
    }
}