using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Exceptions.Attendance;
using OfficeAttendance.Core.Interfaces;

namespace OfficeAttendance.Application.UseCases.Attendance;

public class GetAttendanceByDayUseCase(IAttendanceRepository attendanceRepository) {
    public async Task<GetByDayResponse> ExecuteAsync(GetByDayRequest request, CancellationToken ct) {
        ct.ThrowIfCancellationRequested();

        try {
            IEnumerable<Employee> attendanceRecords = await attendanceRepository.GetByDay(request.Date, ct);
            return new GetByDayResponse { Employees = attendanceRecords };
        }
        catch (Exception ex) {
            throw new AttendanceNotFoundException("Failed to get attendance by day", ex);
        }
    }
}
