using OfficeAttendanceAPI.src.Application.DTOs.Attendance;
using OfficeAttendanceAPI.src.Core.Exceptions.Attendance;
using OfficeAttendanceAPI.src.Core.Interfaces;

namespace OfficeAttendanceAPI.src.Application.UseCases.Attendance;
public class GetAttendanceByWeekUseCase(IAttendanceRepository attendanceRepository)
{
    public async Task<GetByWeekResponse> ExecuteAsync(CancellationToken ct)
    {
        try
        {
            var attendanceRecords = await attendanceRepository.GetByWeek(ct);
            return new GetByWeekResponse{ Employees = attendanceRecords };
        }
        catch (Exception ex)
        {
            throw new AttendanceNotFoundException("Failed to get attendance by week", ex);
        }
    }
}