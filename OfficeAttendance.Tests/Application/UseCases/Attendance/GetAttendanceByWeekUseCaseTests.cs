using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Exceptions.Attendance;
using OfficeAttendance.Tests.Application.Fakes;

namespace OfficeAttendance.Tests.Application.UseCases.Attendance;

public class GetAttendanceByWeekUseCaseTests {
    private readonly FakeAttendanceRepository _attendanceRepository;
    private readonly GetAttendanceByWeekUseCase _useCase;
    private readonly CancellationToken _cancellationToken;

    public GetAttendanceByWeekUseCaseTests() {
        _attendanceRepository = new FakeAttendanceRepository();
        _useCase = new GetAttendanceByWeekUseCase(_attendanceRepository);
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnAttendance_WhenValidRequestIsPassed() {
        // Arrange
        var mockAttendance = new List<AttendanceReport>
        {
            new() {
                Date = new DateOnly(2024, 09, 2),
                Employees =
                [
                    new() { Id = 1, FirstName = "Dante", LastName = "Alighieri" }
                ]
            },
        };

        _attendanceRepository.SetAttendanceForWeek(mockAttendance, 1);

        // Act
        GetByWeekResponse result = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockAttendance.Count, result.AttendanceReport.Count());
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnEmptyAttendance_WhenNoEmployeesAttended() {
        // Arrange
        _attendanceRepository.SetAttendance(new List<Employee>());

        // Act
        GetByWeekResponse result = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.AttendanceReport);
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldThrowException_WhenRepositoryThrowsException() {
        // Arrange
        _attendanceRepository.ShouldThrowException = true;

        // Act
        async Task Act() => await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        _ = await Assert.ThrowsAsync<AttendanceNotFoundException>(Act);
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldRespectCancellationToken_WhenRepositoryReturnsNull() {
        // Arrange
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        // Act
        async Task Act() => await _useCase.ExecuteAsync(cancellationTokenSource.Token);

        // Assert
        _ = await Assert.ThrowsAsync<OperationCanceledException>(Act);
        Assert.False(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnCorrectAttendance_WhenDifferentWeeksAreQueried() {
        // Arrange
        var week1Attendance = new List<AttendanceReport>
        {
            new() {
                Date = new DateOnly(2024, 09, 2),
                Employees =
                [
                    new() { Id = 1, FirstName = "Dante", LastName = "Alighieri" },
                    new() { Id = 2, FirstName = "Italo", LastName = "Calvino" }
                ]
            },
            new() {
                Date = new DateOnly(2024, 09, 4),
                Employees =
                [
                    new() { Id = 3, FirstName = "Giovanni", LastName = "Boccaccio" },
                    new() { Id = 4, FirstName = "Petrarch", LastName = "Francesco" }
                ]
            }
        };
        
        var week2Attendance = new List<AttendanceReport>
        {
            new() {
                Date = new DateOnly(2024, 09, 9),
                Employees =
                [
                    new Employee { Id = 5, FirstName = "Julio", LastName = "Cortazar" }
                ]
            }
        };

        _attendanceRepository.SetAttendanceForWeek(week1Attendance, 1);
        _attendanceRepository.SetAttendanceForWeek(week2Attendance, 2);

        // Act
        GetByWeekResponse resultWeek1 = await _useCase.ExecuteAsync(_cancellationToken);
        _attendanceRepository.SetCurrentWeek(2);
        GetByWeekResponse resultWeek2 = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(resultWeek1);
        Assert.Equal(week1Attendance.Count, resultWeek1.AttendanceReport.Count());

        Assert.NotNull(resultWeek2);
        Assert.Equal(week2Attendance.Count, resultWeek2.AttendanceReport.Count());
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnEmpty_WhenRepositoryReturnsNull() {
        // Arrange
        _attendanceRepository.SetAttendance(null);

        // Act
        GetByWeekResponse result = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.AttendanceReport);
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }
}
