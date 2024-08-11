using System;

namespace OfficeAttendanceAPI.src.Core.Exceptions.Employee;

public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException(int id, Exception innerException) : base($"Employee with id {id} was not found.", innerException)
    {
    }

    public EmployeeNotFoundException(string message) : base(message)
    {
    }

    public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}