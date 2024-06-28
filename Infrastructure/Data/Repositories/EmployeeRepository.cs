using Microsoft.EntityFrameworkCore;
using OfficeAttendanceAPI.Core.Entities;
using OfficeAttendanceAPI.Application.Interfaces;
using OfficeAttendanceAPI.Core.Exceptions;

namespace OfficeAttendanceAPI.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<EmployeeRepository> _logger;
        public EmployeeRepository(AppDbContext dbContext, ILogger<EmployeeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Employee> CreateEmployee(Employee employee, CancellationToken ct)
        {
            try
            {
                var entity = await _dbContext.Employees.AddAsync(employee, ct);
                await _dbContext.SaveChangesAsync(ct);
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new EmployeeCreationException("Failed to create employees", ex);
            }
        }

        public async Task<Employee> RemoveEmployee(int id, CancellationToken ct)
        {
            try
            {
                var employee = await _dbContext.Employees.FindAsync(id , ct);
                if (employee == null)
                {
                    throw new EmployeeNotFoundException($"Failed to get remove with id: {id}. Employee not found.");
                }
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync(ct);
                return employee;
            }
            catch (Exception ex)
            {
                throw new EmployeeDeletionException($"Failed to get remove with id: {id}", ex);
            }
        }

        public async Task<Employee?> GetEmployeeById(int id, CancellationToken ct)
        {
            try
            {
                return await _dbContext.Employees.FindAsync(id, ct);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException(id, ex);
            }
        }

        public async Task<IEnumerable<Employee?>> GetEmployees(CancellationToken ct)
        {
            try
            {
                return await _dbContext.Employees.ToListAsync(ct);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Failed to get employees", ex);
            }
        }
    }
}