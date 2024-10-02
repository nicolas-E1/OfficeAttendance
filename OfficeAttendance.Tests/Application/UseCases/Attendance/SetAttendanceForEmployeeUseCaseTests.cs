using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Exceptions.Attendance;
using OfficeAttendance.Tests.Application.Fakes;

namespace OfficeAttendance.Tests.Application.UseCases.Attendance;
public class SetAttendanceForEmployeeUseCaseTests {
    private readonly FakeAttendanceRepository _attendanceRepository;
    private readonly SetAttendanceForEmployeeUseCase _useCase;

    public SetAttendanceForEmployeeUseCaseTests() {
        _attendanceRepository = new FakeAttendanceRepository();
        _useCase = new SetAttendanceForEmployeeUseCase(_attendanceRepository);
    }

    [Fact]
    public async Task SetAttendanceForEmployeeUseCase_ShouldReturnAttendance_WhenValidRequestIsPassed() {
        // Arrange
        var mockEmployee = new Employee {
            Id = 1,
            FirstName = "Dante",
            LastName = "Alighieri",
        };
        var mockDate = new DateOnly(2024, 09, 2);


        // Act
        SetAttendanceResponse result = await _useCase.ExecuteAsync(new SetAttendanceRequest {
            EmployeeId = mockEmployee.Id,
            Date = mockDate
        }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockEmployee.Id, result.Employee.Id);
        Assert.Equal(mockEmployee.FirstName, result.Employee.FirstName);
        Assert.Equal(mockEmployee.LastName, result.Employee.LastName);
        Assert.Equal(mockEmployee.FullName, result.Employee.FullName);
        Assert.Equal(mockDate, result.Date);
    }

    [Fact]
    public async Task SetAttendanceForEmployeeUseCase_ShouldThrowSetAttendanceException_WhenRepositoryThrowsException() {
        // Arrange
        _attendanceRepository.ShouldThrowException = true;

        // Act
        async Task action() => await _useCase.ExecuteAsync(new SetAttendanceRequest {
            EmployeeId = 1,
            Date = new DateOnly(2024, 09, 2)
        }, CancellationToken.None);

        // Assert
        _ = await Assert.ThrowsAsync<SetAttendanceException>(action);
    }

    [Fact]
    public async Task SetAttendanceForEmployeeUseCase_ShouldThrowOperationCanceledException_WhenRequestIsCancelled() {
        // Arrange
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act
        async Task action() => await _useCase.ExecuteAsync(new SetAttendanceRequest {
            EmployeeId = 1,
            Date = new DateOnly(2024, 09, 2)
        }, cts.Token);

        // Assert
        _ = await Assert.ThrowsAsync<OperationCanceledException>(action);
    }
}
