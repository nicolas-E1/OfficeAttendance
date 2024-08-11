using FastEndpoints;
using OfficeAttendanceAPI.src.Application.DTOs.Attendance;
using OfficeAttendanceAPI.src.Core.Interfaces;
using OfficeAttendanceAPI.src.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.src.Application.Endpoints.Employee.Attendance;

public class GetAttendanceByDayEndpoint(IAttendanceRepository attendanceRepository) : Endpoint<GetAttendanceByDayRequest, GetAttendanceByDayResponse>
{
    public override void Configure()
    {
        Get("/attendance/reports/day/{Date}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAttendanceByDayRequest req, CancellationToken ct)
    {
        try
        {
            var attendances = await attendanceRepository.GetByDay(req.Date, ct);
            var response =new GetAttendanceByDayResponse{ Employees = attendances };

            Response.Employees = attendances;
        }
        catch (Exception ex)
        {
            throw new AttendanceNotFoundException("Failed to get attendance by day", ex);
        }
    }
}