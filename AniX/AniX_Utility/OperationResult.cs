namespace AniX_Utility;

public class OperationResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public OperationResult()
    {
        Success = false;
        Message = string.Empty;
    }

    public OperationResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}