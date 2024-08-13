using OfficeAttendanceAPI.Core.Interfaces;
using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.Application.UseCases.Attendance;
public class GetAttendanceByDayUseCase(IAttendanceRepository attendanceRepository)
{
    public async Task<GetByDayResponse> ExecuteAsync(GetByDayRequest request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        try
        {
            var attendanceRecords = await attendanceRepository.GetByDay(request.Date, ct);
            return new GetByDayResponse{ Employees = attendanceRecords };
        }
        catch (Exception ex)
        {
            throw new AttendanceNotFoundException("Failed to get attendance by day", ex);
        }
    }
}
