using System.ComponentModel.DataAnnotations;

namespace OfficeAttendance.Application.DTOs.Attendance;
public class GetByDayRequest {
    [Required]
    public DateOnly Date { get; set; }
}
