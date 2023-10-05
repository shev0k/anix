using System;
using System.IO;
using System.Reflection;

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
        private static string GetSolutionDirectory()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(assemblyLocation));

            while (directoryInfo.Name != "AniX")
            {
                directoryInfo = directoryInfo.Parent;
            }

            return directoryInfo.FullName;
        }

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
            string solutionDirectory = GetSolutionDirectory();

            string filePath = Path.Combine(solutionDirectory, "ErrorLog.txt");

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
            string solutionDirectory = GetSolutionDirectory();
            string filePath = Path.Combine(solutionDirectory, "FallbackErrorLog.txt");

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
                // oopsie, this is a critical failure
            }
        }
    }
}
