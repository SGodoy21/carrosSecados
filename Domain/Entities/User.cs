using System;

namespace Domain.Entities;

/// <summary>
/// Domain entity representing a system user.
/// </summary>
public class User
{
    public long Id { get; set; }

    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public bool IsEnabled { get; set; } = true;
    public int FailedAccessCount { get; set; }

    public int GroupId { get; set; }
    public long ClientId { get; set; }

    public string RecoveryToken { get; set; } = default!;
    public DateTime? TokenExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }

    // FK
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
}
