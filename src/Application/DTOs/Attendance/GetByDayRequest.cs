using System.ComponentModel.DataAnnotations;

namespace OfficeAttendanceAPI.src.Application.DTOs.Attendance;
public class GetAttendanceByDayRequest
{
    [Required]
    public DateOnly Date { get; set; }
}