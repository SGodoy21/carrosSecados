namespace Shared.Responses;

/// <summary>
/// Standard API response wrapper.
/// </summary>
public sealed class AxResponse<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }

    // Ok con data opcional y mensaje opcional
    public static AxResponse<T> Ok(T? data = default, string? message = null) => new()
    {
        Success = true,
        Message = message,
        Data = data
    };

    public static AxResponse<T> Fail(string message) => new()
    {
        Success = false,
        Message = message,
        Data = default
    };
}