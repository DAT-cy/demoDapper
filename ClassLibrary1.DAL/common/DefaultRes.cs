using System;

public class DefaultRes<T>
{
    public int StatusCode { get; set; }

    public string ResponseMessage { get; set; }

    public T Data { get; set; }

    // Constructor
    public DefaultRes(int statusCode, string responseMessage)
    {
        StatusCode = statusCode;
        ResponseMessage = responseMessage;
        Data = default(T);
    }

    // Constructor with data
    public DefaultRes(int statusCode, string responseMessage, T data)
    {
        StatusCode = statusCode;
        ResponseMessage = responseMessage;
        Data = data;
    }

    // Static method to return response with or without data
    public static DefaultRes<T> Res(int statusCode, string responseMessage)
    {
        return Res(statusCode, responseMessage, default(T));
    }

    public static DefaultRes<T> Res(int statusCode, string responseMessage, T data)
    {
        return new DefaultRes<T>(statusCode, responseMessage, data);
    }
}