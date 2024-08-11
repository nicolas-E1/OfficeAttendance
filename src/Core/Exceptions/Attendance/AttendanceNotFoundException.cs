namespace OfficeAttendanceAPI.src.Core.Exceptions.Attendance;

public class AttendanceNotFoundException : Exception
{
    public AttendanceNotFoundException(string message) : base(message)
    {
    }

    public AttendanceNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}