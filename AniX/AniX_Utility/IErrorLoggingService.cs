namespace AniX_Utility;

public interface IErrorLoggingService
{
    Task LogErrorAsync(Exception e, LogSeverity severity = LogSeverity.Error);
    Task LogCustomMessageAsync(string customMessage, LogSeverity severity = LogSeverity.Info);
    Task FallbackLoggingAsync(Exception e, LogSeverity severity);
    Task AuditLogAsync(string action, string details, LogSeverity severity = LogSeverity.Info);
    string GetLogFilePath();
}