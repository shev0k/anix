namespace AniX_Utility;

public interface IExceptionHandlingService
{
    Task<bool> HandleExceptionAsync(Exception e);
}