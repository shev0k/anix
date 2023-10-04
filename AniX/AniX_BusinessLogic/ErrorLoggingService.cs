using System;
using System.IO;

public static class ErrorLoggingService
{
    public static void LogError(Exception e)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        string filePath = Path.Combine(desktopPath, "ErrorLog.txt");

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Timestamp: {DateTime.Now}");
            writer.WriteLine($"Exception Type: {e.GetType().FullName}");
            writer.WriteLine($"Message: {e.Message}");
            writer.WriteLine($"Stack Trace: {e.StackTrace}");
            writer.WriteLine("---------------------------------------------------");
        }
    }
}
