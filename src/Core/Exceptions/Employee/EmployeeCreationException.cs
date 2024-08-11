namespace OfficeAttendanceAPI.src.Core.Exceptions.Employee;

public class EmployeeCreationException(string message, Exception innerException)
    : Exception(message, innerException);