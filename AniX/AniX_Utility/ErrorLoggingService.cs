using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AniX_Utility
{
    public class ErrorLoggingService : IErrorLoggingService
    {
        private string GetSolutionDirectory()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(assemblyLocation));

            while (directoryInfo.Name != "AniX")
            {
                directoryInfo = directoryInfo.Parent;
            }

            return directoryInfo.FullName;
        }

        public async Task LogErrorAsync(Exception e, LogSeverity severity = LogSeverity.Error)
        {
            string formattedMessage = $"Exception Type: {e.GetType().FullName}\n" +
                                      $"Message: {e.Message}\n" +
                                      $"Stack Trace: {e.StackTrace}";
            await LogMessageAsync(formattedMessage, severity);
        }

        public async Task LogCustomMessageAsync(string customMessage, LogSeverity severity = LogSeverity.Info)
        {
            string formattedMessage = $"Custom Message: {customMessage}";
            await LogMessageAsync(formattedMessage, severity);
        }

        private async Task LogMessageAsync(string message, LogSeverity severity)
        {
            string solutionDirectory = GetSolutionDirectory();
            string filePath = Path.Combine(solutionDirectory, "ErrorLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync($"Timestamp: {DateTime.Now}");
                    await writer.WriteLineAsync($"Severity: {severity}");
                    await writer.WriteLineAsync(message);
                    await writer.WriteLineAsync("---------------------------------------------------");
                }
            }
            catch
            {
                await FallbackLoggingAsync(new Exception("Failed to log message"), LogSeverity.Critical);
            }
        }

        public async Task FallbackLoggingAsync(Exception e, LogSeverity severity)
        {
            string solutionDirectory = GetSolutionDirectory();
            string filePath = Path.Combine(solutionDirectory, "FallbackErrorLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync($"Timestamp: {DateTime.Now}");
                    await writer.WriteLineAsync($"Severity: {severity}");
                    await writer.WriteLineAsync($"Exception Type: {e.GetType().FullName}");
                    await writer.WriteLineAsync($"Message: {e.Message}");
                    await writer.WriteLineAsync($"Stack Trace: {e.StackTrace}");
                    await writer.WriteLineAsync("---------------------------------------------------");
                }
            }
            catch
            {
            }
        }

        public string GetLogFilePath()
        {
            return Path.Combine(GetSolutionDirectory(), "ErrorLog.txt");
        }

        public async Task AuditLogAsync(string action, string details, LogSeverity severity = LogSeverity.Info)
        {
            await LogMessageAsync($"Action: {action}\nDetails: {details}", severity);
        }
    }
}
