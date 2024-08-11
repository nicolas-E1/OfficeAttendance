using OfficeAttendanceAPI.src.Core.Interfaces;
using OfficeAttendanceAPI.src.Application.DTOs.Attendance;
using OfficeAttendanceAPI.src.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.src.Application.UseCases.Attendance;
public class GetAttendanceByDayUseCase(IAttendanceRepository attendanceRepository)
{
    public async Task<GetByDayResponse> ExecuteAsync(GetByDayRequest request, CancellationToken ct)
    {
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