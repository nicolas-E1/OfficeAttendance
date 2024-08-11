using FastEndpoints;
using OfficeAttendanceAPI.src.Application.DTOs.Attendance;
using OfficeAttendanceAPI.src.Core.Interfaces;
using OfficeAttendanceAPI.src.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.src.Application.Endpoints.Employee.Attendance;

public class GetAttendanceByWeekEndpoint(IAttendanceRepository attendanceRepository) : EndpointWithoutRequest<GetAttendanceByWeekResponse>
{
    public override void Configure()
    {
        Get("/attendance/reports/week");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var attendances = await attendanceRepository.GetByWeek(ct);
            var response =new GetAttendanceByWeekResponse{ Employees = attendances };
            Response.Employees = attendances;
        }
        catch (Exception ex)
        {
            throw new AttendanceNotFoundException("Failed to get attendance by week", ex);
        }
    }
}