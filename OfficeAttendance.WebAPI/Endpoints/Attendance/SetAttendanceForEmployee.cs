using FastEndpoints;
using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;

namespace OfficeAttendance.WebAPI.Endpoints.Attendance;

public class SetAttendanceForEmployeeEndpoint(SetAttendanceForEmployeeUseCase setAttendanceForEmployeeUseCase) : Endpoint<SetAttendanceRequest, SetAttendanceResponse> {
    public override void Configure() {
        Post("/attendance/confirm");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SetAttendanceRequest request, CancellationToken ct) {
        SetAttendanceResponse response = await setAttendanceForEmployeeUseCase.ExecuteAsync(request, ct);
        await SendAsync(response, cancellation: ct);
    }
}
