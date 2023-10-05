using AniX_Utility;
using System;
using System.Threading.Tasks;

namespace AniX_BusinessLogic
{
    public class AuditService
    {
        public async Task LogLoginAttemptAsync(string username, bool isSuccess, string additionalInfo = "")
        {
            try
            {
                string logMessage = $"Timestamp: {DateTime.Now}, Username: {username}, Success: {isSuccess}, Additional Info: {additionalInfo}";
                await ErrorLoggingService.LogCustomMessageAsync(logMessage); 
            }
            catch (Exception e)
            {
                await ExceptionHandlingService.HandleExceptionAsync(e);  
                throw;
            }
        }
    }
}