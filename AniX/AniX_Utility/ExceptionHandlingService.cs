using System;
using System.Threading.Tasks;

namespace AniX_Utility
{
    public static class ExceptionHandlingService
    {
        public static async Task<bool> HandleExceptionAsync(Exception e)
        {
            try
            {
                await ErrorLoggingService.LogErrorAsync(e, LogSeverity.Error);
                return true; // logs error
            }
            catch
            {
                // alternative logging
                try
                {
                    await ErrorLoggingService.FallbackLoggingAsync(e, LogSeverity.Critical);
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