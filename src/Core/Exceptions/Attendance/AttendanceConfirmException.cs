namespace OfficeAttendanceAPI.Core.Exceptions.Attendance
{
    public class AttendanceConfirmException : Exception
    {
        public AttendanceConfirmException(string message) : base(message)
        {
        }

        public AttendanceConfirmException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}