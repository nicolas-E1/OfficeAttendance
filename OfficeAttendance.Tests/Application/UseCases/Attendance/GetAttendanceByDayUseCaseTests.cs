using OfficeAttendance.Application.DTOs.Attendance;
using OfficeAttendance.Application.UseCases.Attendance;
using OfficeAttendance.Core.Entities;
using OfficeAttendance.Core.Exceptions.Attendance;
using OfficeAttendance.Tests.Application.Fakes;

namespace OfficeAttendance.Tests.Application.UseCases.Attendance;

public class GetAttendanceByDayUseCaseTests {
    private readonly FakeAttendanceRepository _attendanceRepository;
    private readonly GetAttendanceByDayUseCase _useCase;
    private readonly CancellationToken _cancellationToken;
    private readonly GetByDayRequest _request = new() { Date = new DateOnly(2024, 1, 1) };

    public GetAttendanceByDayUseCaseTests() {
        _attendanceRepository = new FakeAttendanceRepository();
        _useCase = new GetAttendanceByDayUseCase(_attendanceRepository);
        _cancellationToken = new CancellationToken();
        _request = new GetByDayRequest { Date = new DateOnly(2024, 1, 1) };
    }

    [Fact]
    public async Task GetAttendanceByDayUseCase_ShouldReturnAttendance_WhenValidRequestIsPassed() {
        // Arrange
        var mockAttendance = new List<Employee>
        {
            new() { Id = 1, FirstName = "Dante", LastName = "Alighieri" },
        };
        _attendanceRepository.SetAttendance(mockAttendance, _request.Date);

        // Act
        GetByDayResponse result = await _useCase.ExecuteAsync(_request, _cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockAttendance.Count, result.Employees.Count());
        Assert.True(_attendanceRepository.WasGetByDayCalled);
    }

    [Fact]
    public async Task GetAttendanceByDayUseCase_ShouldReturnEmptyAttendance_WhenNoEmployeesAttended() {
        // Arrange
        _attendanceRepository.SetAttendance(new List<Employee>(), _request.Date);

        // Act
        GetByDayResponse result = await _useCase.ExecuteAsync(_request, _cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Employees);
        Assert.True(_attendanceRepository.WasGetByDayCalled);
    }

    [Fact]
    public async Task GetAttendanceByDayUseCase_ShouldThrowException_WhenRepositoryThrowsException() {
        // Arrange
        _attendanceRepository.ShouldThrowException = true;

        // Act
        async Task Act() => await _useCase.ExecuteAsync(_request, _cancellationToken);

        // Assert
        _ = await Assert.ThrowsAsync<AttendanceNotFoundException>(Act);
        Assert.True(_attendanceRepository.WasGetByDayCalled);
    }

    [Fact]
    public async Task GetAttendanceByDayUseCase_ShouldRespectCancellationToken_WhenRepositoryReturnsNull() {
        // Arrange
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        // Act
        async Task Act() => await _useCase.ExecuteAsync(_request, cancellationTokenSource.Token);

        // Assert
        _ = await Assert.ThrowsAsync<OperationCanceledException>(Act);
        Assert.False(_attendanceRepository.WasGetByDayCalled);
    }

    [Fact]
    public async Task GetAttendanceByDayUseCase_ShouldReturnCorrectAttendance_WhenDifferentWeeksAreQueried() {
        // Arrange
        var day1Attendance = new List<Employee>
        {
            new() { Id = 1, FirstName = "Dante", LastName = "Alighieri" },
            new() { Id = 2, FirstName = "Italo", LastName = "Calvino" },
        };
        var day2Attendance = new List<Employee>
        {
            new() { Id = 3, FirstName = "Julio", LastName = "Cortazar" },
        };

        _attendanceRepository.SetAttendance(day1Attendance, _request.Date);
        _attendanceRepository.SetAttendance(day2Attendance, _request.Date.AddDays(1));

        // Act
        GetByDayResponse resultDay1 = await _useCase.ExecuteAsync(_request, _cancellationToken);
        GetByDayResponse resultDay2 = await _useCase.ExecuteAsync(new GetByDayRequest { Date = _request.Date.AddDays(1) }, _cancellationToken);

        // Assert
        Assert.True(_attendanceRepository.WasGetByDayCalled);

        Assert.NotNull(resultDay1);
        Assert.Equal(day1Attendance.Count(), resultDay1.Employees.Count());

        Assert.NotNull(resultDay2);
        Assert.Equal(day2Attendance.Count(), resultDay2.Employees.Count());
    }

    [Fact]
    public async Task GetAttendanceByDayUseCase_ShouldReturnEmpty_WhenRepositoryReturnsNull() {
        // Arrange
        _attendanceRepository.SetAttendance(null);

        // Act
        GetByDayResponse result = await _useCase.ExecuteAsync(_request, _cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Employees);
        Assert.True(_attendanceRepository.WasGetByDayCalled);
    }
}
