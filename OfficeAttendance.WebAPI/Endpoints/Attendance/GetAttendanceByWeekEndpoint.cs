﻿using FastEndpoints;
using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;

namespace OfficeAttendance.WebAPI.Endpoints.Attendance;

public class GetAttendanceByWeekEndpoint(GetAttendanceByWeekUseCase getAttendanceByWeekUseCase) : EndpointWithoutRequest<GetByWeekResponse> {
    public override void Configure() {
        Get("/attendance/reports/week");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) {
        GetByWeekResponse response = await getAttendanceByWeekUseCase.ExecuteAsync(ct);
        await SendAsync(response, cancellation: ct);
    }
}
