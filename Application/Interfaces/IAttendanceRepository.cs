using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>?> GetByDay();
        Task<IEnumerable<Attendance>?> GetByWeek();
        Task<IEnumerable<Attendance>?> GetById();
        Task<Attendance> ConfirmAttendance(Attendance attendance);
        Task<Attendance> DeleteAttendance(int id);
    }
}