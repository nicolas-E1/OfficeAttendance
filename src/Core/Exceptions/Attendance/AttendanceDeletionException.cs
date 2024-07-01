namespace OfficeAttendanceAPI.Core.Exceptions.Attendance
{
    public class AttendanceDeletionException : Exception
    {
        public AttendanceDeletionException(string message) : base(message)
        {
        }

        public AttendanceDeletionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}