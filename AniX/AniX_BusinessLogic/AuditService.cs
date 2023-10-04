using AniX_Utility;
using System;

namespace AniX_BusinessLogic
{
    public class AuditService
    {
        public void LogLoginAttempt(string username, bool isSuccess, string additionalInfo = "")
        {
            try
            {
                string logMessage = $"Timestamp: {DateTime.Now}, Username: {username}, Success: {isSuccess}, Additional Info: {additionalInfo}";
                ErrorLoggingService.LogCustomMessage(logMessage);
            }
            catch (Exception e)
            {
                ExceptionHandlingService.HandleException(e);
            }
        }
    }
}
