using FastEndpoints;
using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;

namespace OfficeAttendance.WebAPI.Endpoints.Attendance;

public class GetAttendanceByDayEndpoint(GetAttendanceByDayUseCase getAttendanceByDayUseCase) : Endpoint<GetByDayRequest, GetByDayResponse> {
    public override void Configure() {
        Get("/attendance/reports/day/{Date}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByDayRequest request, CancellationToken ct) {
        GetByDayResponse response = await getAttendanceByDayUseCase.ExecuteAsync(request, ct);
        await SendAsync(response, cancellation: ct);
    }
}
