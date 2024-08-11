using System.ComponentModel.DataAnnotations;

namespace OfficeAttendanceAPI.src.Application.DTOs.Attendance;
public class GetByDayRequest
{
    [Required]
    public DateOnly Date { get; set; }
}