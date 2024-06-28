using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User?>> GetUsers();
        Task<User?> GetUser(int id);
        Task<User> AddUser(User user);
        Task<User> DeleteUser(int id);
    }
}