using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using System;
using System.Threading.Tasks;
using AniX_Utility;

namespace AniX_UnitTest
{
    public class ErrorLoggingServiceTests
    {
        private readonly Mock<IErrorLoggingService> _mockErrorLoggingService;

        public ErrorLoggingServiceTests()
        {
            _mockErrorLoggingService = new Mock<IErrorLoggingService>();
        }
        [Fact]
        public async Task LogErrorAsync_CallsLogging()
        {
            var exception = new Exception("Test exception");
            var severity = LogSeverity.Error;

            _mockErrorLoggingService.Setup(service => service.LogErrorAsync(exception, severity))
                .Returns(Task.CompletedTask);

            await _mockErrorLoggingService.Object.LogErrorAsync(exception, severity);

            _mockErrorLoggingService.Verify(service => service.LogErrorAsync(exception, severity), Times.Once);
        }
        [Fact]
        public async Task LogCustomMessageAsync_CallsLogging()
        {
            string customMessage = "Custom log message";
            var severity = LogSeverity.Info;

            _mockErrorLoggingService.Setup(service => service.LogCustomMessageAsync(customMessage, severity))
                .Returns(Task.CompletedTask);

            await _mockErrorLoggingService.Object.LogCustomMessageAsync(customMessage, severity);

            _mockErrorLoggingService.Verify(service => service.LogCustomMessageAsync(customMessage, severity), Times.Once);
        }
        [Fact]
        public async Task FallbackLoggingAsync_CallsLogging()
        {
            var exception = new Exception("Fallback exception");
            var severity = LogSeverity.Critical;

            _mockErrorLoggingService.Setup(service => service.FallbackLoggingAsync(exception, severity))
                .Returns(Task.CompletedTask);

            await _mockErrorLoggingService.Object.FallbackLoggingAsync(exception, severity);

            _mockErrorLoggingService.Verify(service => service.FallbackLoggingAsync(exception, severity), Times.Once);
        }
        [Fact]
        public async Task AuditLogAsync_CallsLogging()
        {
            string action = "UserLogin";
            string details = "User logged in successfully";
            var severity = LogSeverity.Info;

            _mockErrorLoggingService.Setup(service => service.AuditLogAsync(action, details, severity))
                .Returns(Task.CompletedTask);

            await _mockErrorLoggingService.Object.AuditLogAsync(action, details, severity);

            _mockErrorLoggingService.Verify(service => service.AuditLogAsync(action, details, severity), Times.Once);
        }
    }
}