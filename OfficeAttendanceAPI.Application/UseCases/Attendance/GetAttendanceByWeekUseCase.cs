using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;
using OfficeAttendanceAPI.Core.Interfaces;

namespace OfficeAttendanceAPI.Application.UseCases.Attendance;
public class GetAttendanceByWeekUseCase(IAttendanceRepository attendanceRepository)
{
    public async Task<GetByWeekResponse> ExecuteAsync(CancellationToken ct) {
        ct.ThrowIfCancellationRequested();
        
        try {
            var attendanceRecords = await attendanceRepository.GetByWeek(ct);
            return new GetByWeekResponse{ Employees = attendanceRecords };
        }
        catch (Exception ex) {
            throw new AttendanceNotFoundException("Failed to get attendance by week", ex);
        }
    }
}
