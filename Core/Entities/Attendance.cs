namespace OfficeAttendanceAPI.Core.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required DateTime Date { get; set; }
    }
}