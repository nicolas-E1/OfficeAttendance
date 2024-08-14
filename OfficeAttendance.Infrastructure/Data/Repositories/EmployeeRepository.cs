using Microsoft.EntityFrameworkCore;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Interfaces;
using OfficeAttendance.Core.Exceptions.Employee;

namespace OfficeAttendance.Infrastructure.Data.Repositories;

public class EmployeeRepository(AppDbContext dbContext) : IEmployeeRepository {
    public async Task<Employee> CreateEmployee(Employee employee, CancellationToken ct) {
        try {
            var entity = await dbContext.Employees.AddAsync(employee, ct);
            await dbContext.SaveChangesAsync(ct);
            return entity.Entity;
        }
        catch (Exception ex) {
            throw new EmployeeCreationException("Failed to create employees", ex);
        }
    }

    public async Task<Employee> RemoveEmployee(int id, CancellationToken ct) {
        try {
            var employee = await dbContext.Employees.FindAsync(id, ct) ?? throw new EmployeeNotFoundException($"Failed to get remove with id: {id}. Employee not found.");
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync(ct);
            return employee;
        }
        catch (Exception ex) {
            throw new EmployeeDeletionException($"Failed to get remove with id: {id}", ex);
        }
    }

    public async Task<Employee?> GetEmployeeById(int id, CancellationToken ct) {
        try {
            return await dbContext.Employees.FindAsync(id, ct);
        }
        catch (Exception ex) {
            throw new EmployeeNotFoundException(id, ex);
        }
    }

    public async Task<IEnumerable<Employee?>> GetEmployees(CancellationToken ct) {
        try {
            return await dbContext.Employees.ToListAsync(ct);
        }
        catch (Exception ex) {
            throw new EmployeeNotFoundException("Failed to get employees", ex);
        }
    }
}
