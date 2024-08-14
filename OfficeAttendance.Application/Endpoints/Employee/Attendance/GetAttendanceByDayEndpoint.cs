using FastEndpoints;
using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;

namespace OfficeAttendance.Application.Endpoints.Employee.Attendance;

public class GetAttendanceByDayEndpoint(GetAttendanceByDayUseCase getAttendanceByDayUseCase) : Endpoint<GetByDayRequest, GetByDayResponse> {
    public override void Configure() {
        Get("/attendance/reports/day/{Date}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetByDayRequest request, CancellationToken ct) {
        var response = await getAttendanceByDayUseCase.ExecuteAsync(request, ct);
        await SendAsync(response, cancellation: ct);
    }
}
