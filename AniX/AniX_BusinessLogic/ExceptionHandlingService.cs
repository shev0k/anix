using System;

namespace AniX_BusinessLogic
{
    public static class ExceptionHandlingService
    {
        public static void HandleException(Exception e)
        {
            ErrorLoggingService.LogError(e);

        }
    }
}