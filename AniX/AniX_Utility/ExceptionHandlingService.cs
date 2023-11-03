using System;
using System.Threading.Tasks;

namespace AniX_Utility
{
    public class ExceptionHandlingService : IExceptionHandlingService
    {
        private readonly IErrorLoggingService _errorLoggingService;

        public ExceptionHandlingService(IErrorLoggingService errorLoggingService)
        {
            _errorLoggingService = errorLoggingService ?? throw new ArgumentNullException(nameof(errorLoggingService));
        }

        public async Task<bool> HandleExceptionAsync(Exception e)
        {
            try
            {
                await _errorLoggingService.LogErrorAsync(e, LogSeverity.Error);
                return true; // logs error
            }
            catch
            {
                // alternative logging
                try
                {
                    await _errorLoggingService.FallbackLoggingAsync(e, LogSeverity.Critical);
                    return true; // oops, added the log
                }
                catch
                {
                    return false; // big oops, no log for you
                }
            }
        }
    }
}