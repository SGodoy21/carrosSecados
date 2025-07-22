namespace Shared.DTOs;

/// <summary>
/// DTO for new user registration.
/// </summary>
public class UserRegisterDto
{
    public string Username { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int RoleId { get; set; }
    public int GroupId { get; set; }
}
