using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Core.Interfaces;
using OfficeAttendance.Core.Exceptions.Attendance;

namespace OfficeAttendance.Application.UseCases.Attendance;
public class GetAttendanceByWeekUseCase(IAttendanceRepository attendanceRepository) {
    public async Task<GetByWeekResponse> ExecuteAsync(CancellationToken ct) {
        ct.ThrowIfCancellationRequested();

        try {
            var attendanceRecords = await attendanceRepository.GetByWeek(ct);
            return new GetByWeekResponse { Employees = attendanceRecords };
        }
        catch (Exception ex) {
            throw new AttendanceNotFoundException("Failed to get attendance by week", ex);
        }
    }
}
