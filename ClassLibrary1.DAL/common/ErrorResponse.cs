public class ErrorResponse : Exception
{
    public int ErrorCode { get; set; }
    public string FriendlyMessage { get; set; }

    public ErrorResponse(ErrorCode errorCode)
        : base(errorCode.Message)
    {
        ErrorCode = errorCode.StatusCode;
        FriendlyMessage = errorCode.FriendlyMessage;
    }
}