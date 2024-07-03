using FastEndpoints;
using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Application.Interfaces;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.Application.Endpoints.Employee.Attendance
{
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
}