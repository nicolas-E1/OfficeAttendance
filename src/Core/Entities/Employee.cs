namespace OfficeAttendanceAPI.Core.Entities
{
    public class Employee
    {
        public int Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public string FullName => $"{FirstName} {LastName}";
    }
}