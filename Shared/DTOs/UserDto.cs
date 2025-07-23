namespace Shared.DTOs;

/// <summary>
/// DTO for public user response.
/// </summary>
public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int RoleId { get; set; }
    public int GroupId { get; set; }
    public bool IsEnabled { get; set; }
}
