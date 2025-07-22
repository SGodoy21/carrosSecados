namespace Shared.Responses;

/// <summary>
/// Standard API response wrapper.
/// </summary>
public class AxResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static AxResponse<T> Ok(T data, string message = "") => new()
    {
        Success = true,
        Message = message,
        Data = data
    };

    public static AxResponse<T> Fail(string message) => new()
    {
        Success = false,
        Message = message
    };
}
    