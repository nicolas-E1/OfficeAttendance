namespace OfficeAttendanceAPI.src.Core.Exceptions.Attendance;

public class AttendanceConfirmException(string message, Exception innerException)
    : Exception(message, innerException);