
namespace DataLayer;

public static class UserSeed
{
    public static List<User> All =>
    [
        new()
        {
            Id = 1,
            Email = "info@dimoland.com",
            FailedLoginCount = 0,
            IsLockedOut = false,
            Mobile = "09309309393",
            Username = "admin-user",
            PasswordHash = PasswordHasher.Hash("QAZqaz!@#123"),
            ConcurrencyStamp = StampGenerator.CreateSecurityStamp(32),
            SecurityStamp = StampGenerator.CreateSecurityStamp(32),
            CreatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
            UpdatedAt = new DateTime(2025, 1, 1, 12, 0, 0),
            Slug= "Admin-User",
            IsActive = true
        }
    ];
}