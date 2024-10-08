﻿using OfficeAttendance.Core.Entities;

namespace OfficeAttendance.Core.Interfaces;

public interface IEmployeeRepository {
    Task<IEnumerable<Employee?>> GetEmployees(CancellationToken ct);
    Task<Employee?> GetEmployeeById(int id, CancellationToken ct);
    Task<Employee> CreateEmployee(Employee employee, CancellationToken ct);
    Task<Employee> RemoveEmployee(int id, CancellationToken ct);
}
