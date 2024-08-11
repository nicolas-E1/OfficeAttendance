namespace OfficeAttendanceAPI.src.Core.Exceptions.Employee;

public class EmployeeDeletionException : Exception
{
    public EmployeeDeletionException(string message) : base(message)
    {
    }

    public EmployeeDeletionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}