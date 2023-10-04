using System;
using System.IO;

namespace AniX_Utility
{
    public enum LogSeverity
    {
        Info,
        Warning,
        Error,
        Critical
    }

    public static class ErrorLoggingService
    {
        public static void LogError(Exception e, LogSeverity severity = LogSeverity.Error)
        {
            LogMessage(
                $"Exception Type: {e.GetType().FullName}\n" +
                $"Message: {e.Message}\n" +
                $"Stack Trace: {e.StackTrace}",
                severity
            );
        }

        public static void LogCustomMessage(string customMessage, LogSeverity severity = LogSeverity.Info)
        {
            LogMessage($"Custom Message: {customMessage}", severity);
        }

        private static void LogMessage(string message, LogSeverity severity)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "ErrorLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"Timestamp: {DateTime.Now}");
                    writer.WriteLine($"Severity: {severity}");
                    writer.WriteLine(message);
                    writer.WriteLine("---------------------------------------------------");
                }
            }
            catch
            {
                FallbackLogging(new Exception("Failed to log message"), LogSeverity.Critical);
            }
        }

        public static void FallbackLogging(Exception e, LogSeverity severity)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "FallbackErrorLog.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"Timestamp: {DateTime.Now}");
                    writer.WriteLine($"Severity: {severity}");
                    writer.WriteLine($"Exception Type: {e.GetType().FullName}");
                    writer.WriteLine($"Message: {e.Message}");
                    writer.WriteLine($"Stack Trace: {e.StackTrace}");
                    writer.WriteLine("---------------------------------------------------");
                }
            }
            catch
            {
                // oopsie
            }
        }
    }
}
