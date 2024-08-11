using FastEndpoints;
using OfficeAttendanceAPI.src.Application.DTOs.Attendance;
using OfficeAttendanceAPI.src.Application.UseCases.Attendance;

namespace OfficeAttendanceAPI.src.Application.Endpoints.Employee.Attendance;

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