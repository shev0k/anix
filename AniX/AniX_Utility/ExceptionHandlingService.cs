namespace AniX_Utility
{
    public static class ExceptionHandlingService
    {
        public static bool HandleException(Exception e)
        {
            try
            {
                ErrorLoggingService.LogError(e, LogSeverity.Error);
                return true; // logs error
            }
            catch
            {
                // alternative logging
                try
                {
                    ErrorLoggingService.FallbackLogging(e, LogSeverity.Critical);
                    return true; // oopa add the log
                }
                catch
                {
                    return false; // big oopsie no log for you
                }
            }
        }
    }
}
