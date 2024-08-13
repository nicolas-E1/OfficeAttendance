using FastEndpoints;
using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Application.UseCases.Attendance;

namespace OfficeAttendanceAPI.Application.Endpoints.Employee.Attendance;

public class GetAttendanceByDayEndpoint(GetAttendanceByDayUseCase getAttendanceByDayUseCase) : Endpoint<GetByDayRequest, GetByDayResponse>
{
    public override void Configure()
    {
        Get("/attendance/reports/day/{Date}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByDayRequest request, CancellationToken ct)
    {
        var response = await getAttendanceByDayUseCase.ExecuteAsync(request, ct);
        await SendAsync(response, cancellation: ct);
    }
}
