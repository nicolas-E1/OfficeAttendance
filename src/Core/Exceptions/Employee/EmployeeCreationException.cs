namespace OfficeAttendanceAPI.Core.Exceptions.Employee
{
    public class EmployeeCreationException : Exception
    {
        public EmployeeCreationException(string message) : base(message)
        {
        }

        public EmployeeCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}