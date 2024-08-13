using OfficeAttendanceAPI.Application.UseCases.Attendance;
using OfficeAttendanceAPI.Core.Entities;
using OfficeAttendanceAPI.Tests.Fakes;

namespace OfficeAttendanceAPI.Tests.UseCases.Attendance
{
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
            var result = await _useCase.ExecuteAsync(_cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockAttendance.Count, result.Employees.Count());
            Assert.True(_attendanceRepository.WasGetByWeekCalled);
        }
    }
}
