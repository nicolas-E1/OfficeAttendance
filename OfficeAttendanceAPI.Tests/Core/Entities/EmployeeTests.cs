using OfficeAttendanceAPI.Core.Entities;

namespace OfficeAttendanceAPI.Tests.Core.Entities;

public class EmployeeTests
{
    [Fact]
    public void Employee_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var employee = new Employee { Id = 1, FirstName = "Dante", LastName = "Alighieri" };

        // Assert
        Assert.Equal(1, employee.Id);
        Assert.Equal("Dante", employee.FirstName);
        Assert.Equal("Alighieri", employee.LastName);
    }

    [Fact]
    public void GetFullName_ShouldReturnCorrectFullName()
    {
        // Arrange
        var employee = new Employee { FirstName = "Dante", LastName = "Alighieri" };

        // Act
        var fullName = employee.FullName;

        // Assert
        Assert.Equal("Dante Alighieri", fullName);
    }
}
