using System.ComponentModel.DataAnnotations;

namespace OfficeAttendanceAPI.Application.DTOs.Attendance;
public class GetByDayRequest
{
    [Required]
    public DateOnly Date { get; set; }
}
