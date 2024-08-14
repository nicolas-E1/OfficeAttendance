using FastEndpoints;
using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Application.UseCases.Attendance;

namespace OfficeAttendanceAPI.Application.Endpoints.Employee.Attendance;

public class GetAttendanceByWeekEndpoint(GetAttendanceByWeekUseCase getAttendanceByWeekUseCase) : EndpointWithoutRequest<GetByWeekResponse>
{
    public override void Configure()
    {
        Get("/attendance/reports/week");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await getAttendanceByWeekUseCase.ExecuteAsync(ct);
        await SendAsync(response, cancellation: ct);
    }
}
