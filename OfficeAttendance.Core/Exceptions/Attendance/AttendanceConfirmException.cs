namespace OfficeAttendance.Core.Exceptions.Attendance;

public class SetAttendanceException : Exception {
    public SetAttendanceException(string message) : base(message) {
    }
    public SetAttendanceException(string message, Exception innerException) : base(message, innerException) {
    }
};
