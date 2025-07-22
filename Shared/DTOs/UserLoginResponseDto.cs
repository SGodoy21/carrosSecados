namespace Shared.DTOs;

public class UserLoginResponseDto
{
    public UserDto User { get; set; } = default!;
    public string Token { get; set; } = default!;
}
