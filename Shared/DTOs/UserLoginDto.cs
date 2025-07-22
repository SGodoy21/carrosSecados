namespace Shared.DTOs;

/// <summary>
/// DTO for user login request.
/// </summary>
public class UserLoginDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
