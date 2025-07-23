using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data;

public class LoginCleanContext : DbContext
{
    public LoginCleanContext(DbContextOptions<LoginCleanContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // USERS
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
            entity.Property(e => e.FailedAccessCount).HasDefaultValue(0);
            entity.Property(e => e.GroupId);
            entity.Property(e => e.ClientId);
            entity.Property(e => e.RecoveryToken).HasMaxLength(255);
            entity.Property(e => e.TokenExpiresAt);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            entity.HasOne(e => e.Role)
                  .WithMany(r => r.Users)
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ROLES
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");

            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Role>().HasData(
       new Role { Id = 1, Name = "Admin" },
       new Role { Id = 2, Name = "User" }
   );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "admin123", // IMPORTANTE: usá hash real
                FirstName = "System",
                LastName = "Administrator",
                Phone = "1234567890",
                IsEnabled = true,
                FailedAccessCount = 0,
                GroupId = 1,
                ClientId = 1,
                RecoveryToken = "",
                TokenExpiresAt = null,
                CreatedAt = DateTime.UtcNow,
                RoleId = 1
            }
        );

    }
}
