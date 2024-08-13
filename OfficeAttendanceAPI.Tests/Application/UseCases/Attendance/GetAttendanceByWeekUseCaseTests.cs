﻿using OfficeAttendanceAPI.Application.DTOs.Attendance;
using OfficeAttendanceAPI.Application.UseCases.Attendance;
using OfficeAttendanceAPI.Core.Entities;
using OfficeAttendanceAPI.Core.Exceptions.Attendance;
using OfficeAttendanceAPI.Tests.Fakes;

namespace OfficeAttendanceAPI.Tests.Application.UseCases.Attendance;

public class GetAttendanceByWeekUseCaseTests
{
    private readonly FakeAttendanceRepository _attendanceRepository;
    private readonly GetAttendanceByWeekUseCase _useCase;
    private readonly CancellationToken _cancellationToken;

    public GetAttendanceByWeekUseCaseTests()
    {
        _attendanceRepository = new FakeAttendanceRepository();
        _useCase = new GetAttendanceByWeekUseCase(_attendanceRepository);
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnAttendance_WhenValidRequestIsPassed()
    {
        // Arrange
        var mockAttendance = new List<Employee>
        {
            new Employee { Id = 1, FirstName = "Dante", LastName = "Alighieri" },
        };
        _attendanceRepository.SetAttendance(mockAttendance);

        // Act
        GetByWeekResponse result = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockAttendance.Count, result.Employees.Count());
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnEmptyAttendance_WhenNoEmployeesAttended()
    {
        // Arrange
        _attendanceRepository.SetAttendance(new List<Employee>());

        // Act
        GetByWeekResponse result = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Employees);
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldThrowException_WhenRepositoryThrowsException()
    {
        // Arrange
        _attendanceRepository.ShouldThrowException = true;

        // Act
        async Task Act() => await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        _ = await Assert.ThrowsAsync<AttendanceNotFoundException>(Act);
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldRespectCancellationToken_WhenRepositoryReturnsNull()
    {
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
    public async Task GetAttendanceByWeekUseCase_ShouldReturnCorrectAttendance_WhenDifferentWeeksAreQueried()
    {
        // Arrange
        var week1Attendance = new List<Employee>
        {
            new() { Id = 1, FirstName = "Dante", LastName = "Alighieri" },
            new() { Id = 2, FirstName = "Italo", LastName = "Calvino" },
        };
        var week2Attendance = new List<Employee>
        {
            new() { Id = 3, FirstName = "Julio", LastName = "Cortazar" },
        };

        _attendanceRepository.SetAttendanceForWeek(week1Attendance, 1);
        _attendanceRepository.SetAttendanceForWeek(week2Attendance, 2);

        // Act
        GetByWeekResponse resultWeek1 = await _useCase.ExecuteAsync(_cancellationToken);
        _attendanceRepository.SetCurrentWeek(2);
        GetByWeekResponse resultWeek2 = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(resultWeek1);
        Assert.Equal(week1Attendance.Count(), resultWeek1.Employees.Count());

        Assert.NotNull(resultWeek2);
        Assert.Equal(week2Attendance.Count(), resultWeek2.Employees.Count());
    }

    [Fact]
    public async Task GetAttendanceByWeekUseCase_ShouldReturnEmpty_WhenRepositoryReturnsNull()
    {
        // Arrange
        _attendanceRepository.SetAttendance(null);

        // Act
        GetByWeekResponse result = await _useCase.ExecuteAsync(_cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Employees);
        Assert.True(_attendanceRepository.WasGetByWeekCalled);
    }
}