using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Exceptions.Attendance;
using OfficeAttendance.Core.Interfaces;

namespace OfficeAttendance.Application.UseCases.Attendance;

public class SetAttendanceForEmployeeUseCase(IAttendanceRepository attendanceRepository) {
    public async Task<SetAttendanceResponse> ExecuteAsync(SetAttendanceRequest request, CancellationToken ct) {
        ct.ThrowIfCancellationRequested();

        try {
            AttendanceReport attendanceRecord = await attendanceRepository.ConfirmAttendance(new Core.Entities.Attendance {
                EmployeeId = request.EmployeeId,
                Date = request.Date
            }, ct);

            return new SetAttendanceResponse {
                Employee = attendanceRecord.Employees.First(),
                Date = attendanceRecord.Date
            };
        }
        catch (Exception ex) {
            throw new SetAttendanceException("Failed to set attendance for employee", ex);
        }
    }
}
