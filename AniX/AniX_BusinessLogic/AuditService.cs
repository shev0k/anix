using AniX_Utility;
using System;
using System.Threading.Tasks;

namespace AniX_BusinessLogic
{
    public class AuditService
    {
        private readonly IErrorLoggingService _errorLoggingService;
        private readonly IExceptionHandlingService _exceptionHandlingService;

        public AuditService(IErrorLoggingService errorLoggingService,
            IExceptionHandlingService exceptionHandlingService)
        {
            _errorLoggingService = errorLoggingService ?? throw new ArgumentNullException(nameof(errorLoggingService));
            _exceptionHandlingService = exceptionHandlingService ??
                                        throw new ArgumentNullException(nameof(exceptionHandlingService));
        }

        public async Task LogLoginAttemptAsync(string username, bool isSuccess, string additionalInfo = "")
        {
            try
            {
                string logMessage =
                    $"Timestamp: {DateTime.Now}, Username: {username}, Success: {isSuccess}, Additional Info: {additionalInfo}";
                await _errorLoggingService.LogCustomMessageAsync(logMessage);
            }
            catch (Exception e)
            {
                await _exceptionHandlingService.HandleExceptionAsync(e);
                throw;
            }
        }
    }
}