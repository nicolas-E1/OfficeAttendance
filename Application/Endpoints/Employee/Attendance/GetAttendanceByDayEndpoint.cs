using FastEndpoints;
using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Application.Interfaces;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;

namespace OfficeAttendanceAPI.Application.Endpoints.Attendance
{
    public class GetAttendanceByDayEndpoint(IAttendanceRepository attendanceRepository) : Endpoint<GetAttendanceByDayRequest, GetAttendanceByDayResponse>
    {
        private readonly IAttendanceRepository _attendanceRepository = attendanceRepository;

        public override void Configure()
        {
            Get("/attendance/day/{Date}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAttendanceByDayRequest req, CancellationToken ct)
        {
            try
            {
                var attendances = await _attendanceRepository.GetByDay(req.Date, ct);
                var response =new GetAttendanceByDayResponse{ Employees = attendances };

                Response.Employees = attendances;
            }
            catch (Exception ex)
            {
                throw new AttendanceNotFoundException("Failed to get attendance by day", ex);
            }
        }
    }
}