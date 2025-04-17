public class ErrorCode
{
    public static readonly ErrorCode STAFF_NOT_FOUND = new ErrorCode(404, "STAFF_NOT_DOUND", "STAFF NOT FOUND");
    public static readonly ErrorCode API_NOT_FOUND = new ErrorCode(404, "API not found", "API không tồn tại");
    public static readonly ErrorCode DEPARTMENT_NOT_FOUND = new ErrorCode(404, "DEPARTMENT_NOT_DOUND", "DEPARTMENT NOT FOUND");

    
    public int StatusCode { get; }
    public string Message { get; }
    public string FriendlyMessage { get; }

    private ErrorCode(int statusCode, string message, string friendlyMessage)
    {
        StatusCode = statusCode;
        Message = message;
        FriendlyMessage = friendlyMessage;
    }
}
